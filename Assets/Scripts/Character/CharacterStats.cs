using Ailment;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utility;
using Utility.EnumType;
using Random = UnityEngine.Random;


namespace Character
{
    public class CharacterStats : MonoBehaviour
    {
        private EntityFX fx;
        [Header("Major Stats")]
        public Stat strength; // 1 point increase damage by 1 and critical power by 1%
        public Stat agility;  //1 point increase evasion by 1% and critical chance by 1%;
        public Stat intelligence;  // increase magic damage
        public Stat vitality;
        public Stat magicalResistance;
        
        [Header("Defensive Stats")]
        public Stat armor;
        public Stat evasion;
        public Stat maxHealth;
        
        [Header("Offensive Stats")] 
        public Stat damage;
        public Stat critChance;
        public Stat critPower;

        [Header("Magic Damage")] 
        public Stat fireDamage;
        public Stat iceDamage;
        public Stat lightningDamage;
        
        public bool isIgnited;
        public bool isChilled; 
        public bool isShocked;

        private float ignitedTimer;
        private float chillTimer;
        private float shockTimer;
        
        private float ignitedDamageCooldown = .3f;
        private float ignitedDamageTimer;
        private float sufferIgnitedDamage;

        [SerializeField] private GameObject lightningPrefab;
        
        /*public  float invulnerableDuration=0.001f;
        private float invulnerableTimer;*/
        public bool canBeHurt { get; private set; } 

        public UnityEvent onDead;
        public float currentHealth;

        protected virtual void Awake()
        {
            fx = GetComponent<EntityFX>();
        }

        protected virtual void Start()
        {
            critPower.SetDefaultValue(150);
            currentHealth = GetMaxHealth();
            canBeHurt = true;
        }

        protected virtual void Update()
        {
            /*GetHurtCounter();*/
            IgnitedCounter();
            
            chillTimer -= Time.deltaTime;
            if (chillTimer < 0)
            {
                isChilled = false;
            }

            shockTimer -= Time.deltaTime;
            if (shockTimer < 0)
            {
                isShocked = false;
            }
            
        }

        /*private void GetHurtCounter()
        {
            if(canBeHurt) return;
            invulnerableTimer -= Time.deltaTime;
            if (invulnerableTimer < 0)
            {
                canBeHurt = true;
                invulnerableTimer = invulnerableDuration;
            }
        }*/

        private void IgnitedCounter()
        {
            ignitedTimer -= Time.deltaTime;
            ignitedDamageTimer -= Time.deltaTime;
            if (ignitedTimer < 0)
            {
                isIgnited = false;
                sufferIgnitedDamage = 0;
            }
            if (ignitedDamageTimer < 0 && isIgnited)
            {
                currentHealth -= sufferIgnitedDamage;
                ignitedDamageTimer = ignitedDamageCooldown;
            }
        }

        //是否可以闪避
        public virtual bool IsEvasion()
        {
            float totalEvasion = evasion.GetValue() + agility.GetValue();
            if (isShocked)
            {
                totalEvasion -= 20;
            }
            if (Random.Range(0f, 100f) <= totalEvasion)
            {
                return true;
            }
            
            return false;
            
        } 
        
        /// <summary>
        /// 被敌人造成伤害 减去血量 有物理伤害， 魔法伤害
        /// </summary>
        /// <param name="originalDamage">初始的武器或者是技能的固定伤害</param>
        /// <param name="enemyStat">敌人的加成属性</param>
        /// <param name="isCrit">敌人是否产生了暴击</param>
        /// <param name="damageType">敌人的攻击类型</param>
        public virtual void MinusHealth(float originalDamage,CharacterStats enemyStat,out bool isCrit,DamageType damageType,AilmentType ailType)
        {
            if (!canBeHurt)
            {
                isCrit = false;
                return;
            }
            SufferAilmentType(ailType,enemyStat);
            
            
            if (enemyStat == null)
            {
                currentHealth -= originalDamage;
                isCrit = false;
                return;
            }
            var originalHealth = currentHealth;
            if(damageType == DamageType.Physical)
                SufferPhysicalDamage(originalDamage, enemyStat, out isCrit);
            else
            {
                SufferMagicalDamage(originalDamage,enemyStat);
                isCrit = false;
            }

            float lostHealth = originalHealth - currentHealth;
            fx.CreatePopUpText($"-{lostHealth}");
            if (currentHealth <= 0)
            {
                Die();
            }

            /*canBeHurt = false;*/
            
        }

        #region Suffer Damage And Ailment

        
        private void SufferAilmentType(AilmentType type,CharacterStats enemy)
        {
            switch (type)
            {
                case AilmentType.Ignite:
                    ApplyAilments(true,false,false,enemy);
                    break;
                case AilmentType.Chill:
                    ApplyAilments(false,true,false,enemy);
                    break;
                case AilmentType.Lightning:
                    ApplyAilments(false,false,true,enemy);
                    break;
                case AilmentType.None:
                    break;
            }
        } 

        private void SufferPhysicalDamage(float originalDamage, CharacterStats enemyStat, out bool isCrit)
        {
            if (enemyStat.canCrit())
            {
                isCrit = true;
                print("ciritcal");
                currentHealth -= GetCriticalDamage(GetFinalDamage(originalDamage,enemyStat.damage),enemyStat.critPower,enemyStat.strength);
            }
            else
            {
                currentHealth -= GetFinalDamage(originalDamage,enemyStat.damage);
                isCrit = false;
            }
            //armor buff calculate
            float armorBuff = armor.GetValue();
            if (isChilled)
            {
                armorBuff *= 0.8f;
            }
            currentHealth = currentHealth + armorBuff > currentHealth? currentHealth:currentHealth+armorBuff;
        }

        private void SufferMagicalDamage(float originalDamage,CharacterStats enemyStat)
        {
            var fire = enemyStat.fireDamage.GetValue();
            var ice = enemyStat.iceDamage.GetValue();
            var lightning = enemyStat.lightningDamage.GetValue();
            float totalMagicDamage = fire + ice+ lightning + enemyStat.intelligence.GetValue()- magicalResistance.GetValue()*0.5f;
            currentHealth -= originalDamage + totalMagicDamage > 0 ? (originalDamage + totalMagicDamage):0;

            if(fire == 0 &&ice == 0 && lightning == 0) return;
            
            bool canApplyIgnite = fire > ice && fire > lightning;
            bool canApplyChill = ice > fire && ice > lightning;
            bool canApplyShock = lightning > fire && lightning > ice;
            if (fire == ice && ice == lightning)
            {
                var value= Random.Range(0, 3);
                if (value == 0) canApplyIgnite = true;
                else if (value == 1) canApplyChill = true;
                else canApplyShock = true;
            }
            ApplyAilments(canApplyIgnite,canApplyChill,canApplyShock,enemyStat);
        }
        #endregion
        
        
        // 获取敌人的最终伤害
        protected virtual float GetFinalDamage(float originalDamage,Stat enemyAttackStat)
        {
            return originalDamage+enemyAttackStat.GetValue();
        }

        // 自身攻击是否可以发生暴击
        protected virtual bool canCrit()
        {
            float totalCriticalChance = critChance.GetValue() + agility.GetValue();
            if (Random.Range(0f, 100f) <= totalCriticalChance)
            {
                return true;
            }

            return false;
        }

        //获取敌人的暴击伤害
        protected virtual float GetCriticalDamage(float originalDamage,Stat enemyCriticalStat,Stat enemyStrength)
        {
            var totalCritPower = enemyCriticalStat.GetValue() + enemyStrength.GetValue();
            return originalDamage * (0.01f * totalCritPower);
        }

        public virtual void ApplyAilments(bool ignite, bool chill, bool shock,CharacterStats enemyStat)
        {
            if (enemyStat == null)
            {
                print("target who cased Ailments is null ");
            }
                
            if (ignite)
            {
                isIgnited = true;
                sufferIgnitedDamage = enemyStat.fireDamage.GetValue() * 0.1f + enemyStat.intelligence.GetValue() * 0.02f;
                ignitedTimer = 2f;
                fx.IgniteFXFor(ignitedTimer);
            }

            if (chill)
            {
                isChilled = true;
                chillTimer = 2f;
                fx.ChillFXFor(chillTimer);
            }

            if (shock)
            {
                isShocked = true;
                shockTimer = 2f;
                fx.ShockFXFor(shockTimer);
                var mask = LayerMask.GetMask("Enemy");
                if (enemyStat is EnemyStats)
                {
                    mask =LayerMask.GetMask("Player");
                }
                    
                var colliders = Physics2D.OverlapCircleAll(transform.position, 20,mask);

                int randomIndex = Random.Range(0, colliders.Length);
                if(enemyStat ==null) return;
                if (colliders[randomIndex] != null)
                {
                    var newLightning = Instantiate(lightningPrefab,transform.position,quaternion.identity);
                    newLightning.transform.position = transform.position;
                    newLightning.GetComponent<ThunderStrikeController>().Setup(enemyStat,colliders[randomIndex].gameObject.transform,GetFinalDamage(0,enemyStat.lightningDamage));
                }
            }
        }

        public float GetMaxHealth()
        {
            return  maxHealth.GetValue()+vitality.GetValue();
        }
        
        protected virtual void Die()
        {
            onDead?.Invoke();
        }

        public virtual void MakeInvincible(bool isInvincible)
        {
            canBeHurt = !isInvincible;
        }

        public virtual string GetStatString(int number)
        {
            switch (number)
            {
                case 0: return GetMajorStatString();
                case 1: return GetDefensiveStatString();
                case 2: return GetMagicalStatString();
                case 3: return GetOffensiveStatString();
            }

            return "";
        }
        
        public virtual string GetMajorStatString()
        {
            return
                $"Strength:{strength.GetValue()} agility:{agility.GetValue()}\nintelligence:{intelligence.GetValue()} vitality:{vitality.GetValue()}";
        }

        public virtual string GetDefensiveStatString()
        {
            return
                $"MaxHealth:{maxHealth.GetValue()+vitality.GetValue()} Armor:{armor.GetValue()}\nEvasion:{evasion.GetValue()}";
        }

        public virtual string GetOffensiveStatString()
        {
            return
                $"Damage:{damage.GetValue()} CriticalChance:{critChance.GetValue()}\nCriticalPower:{critPower.GetValue()}";
        }

        public virtual string GetMagicalStatString()
        {
            return
                $"LightningDamage:{lightningDamage.GetValue()} IceDamage:{iceDamage.GetValue()}\nFireDamage:{fireDamage.GetValue()}";
        }
        
    }
}