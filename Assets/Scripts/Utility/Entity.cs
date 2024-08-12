using System.Collections;
using Character;
using UnityEngine;
using UnityEngine.Events;
using Utility.EnumType;

namespace Utility
{
    public class Entity : MonoBehaviour
    {
        public  Animator anim { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public EntityFX fx { get; private set; }
        public SpriteRenderer sr { get; private set; }
        public CharacterStats stats{ get; private set; }
        public CapsuleCollider2D cd { get; private set; }
        [Header("Check whether on the Ground")]
        [SerializeField] protected LayerMask whatIsGround;
        [SerializeField] protected float checkGroundRadius;
        [SerializeField] protected Transform groundCheck;
        
        [Header("Flip(need to set by manual)")]
        public bool isFacingRight; // need to set in inspector

        public int facingDir;

        [Header("Check Wall")] 
        [SerializeField] protected float checkWallRadius;
        [SerializeField] protected Transform wallCheck;

        [Header("CounterAttack check")] 
        [SerializeField] protected float checkCounterAttackRadius;
        [SerializeField] protected Transform counterAttackCheck;
        
        [Header("knockback Info")] 
        [SerializeField] protected Vector2 knockbackForce;
        [SerializeField] protected bool isKnockback;
        [SerializeField] protected float knockbackDura;
        [SerializeField] protected bool canBeKnocked=true;
        public UnityEvent onFlipped;
        protected virtual void Awake()
        {
            stats = GetComponent<CharacterStats>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
            fx = GetComponent<EntityFX>();
            sr = GetComponentInChildren<SpriteRenderer>();
            cd = GetComponent<CapsuleCollider2D>();
            if (wallCheck == null )
            {
                wallCheck = transform;
            }

            if (counterAttackCheck == null)
            {
                counterAttackCheck = transform;
            }

            if (isFacingRight)
            {
                facingDir = 1;
            }
            else
            {
                facingDir = -1;
            }
        }
        

        protected virtual void Update()
        {
            //CheckPhysics();
        }
        
        public virtual void Flip()
        {
            isFacingRight = !isFacingRight;
            if (isFacingRight) 
                facingDir = 1;
            else
                facingDir = -1;
            
            transform.Rotate(0,180,0);
            onFlipped?.Invoke();
        }
        
       

        public virtual bool CheckGround()
        {
            return  Physics2D.Raycast(groundCheck.position, Vector2.down, checkGroundRadius,whatIsGround);
            
        }

        public virtual bool CheckWall()
        {
           return Physics2D.Raycast(wallCheck.position, Vector2.right*facingDir, checkWallRadius,whatIsGround);
           
        }

        public virtual Collider2D[] GetCounterableEnemy()
        {
            return Physics2D.OverlapCircleAll(counterAttackCheck.position, checkCounterAttackRadius);
        }
        
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,groundCheck.position.y-checkGroundRadius));
            Gizmos.DrawLine(wallCheck.position,new Vector3(wallCheck.position.x+checkWallRadius*facingDir,wallCheck.position.y));
            Gizmos.DrawWireSphere(new Vector3(counterAttackCheck.position.x,counterAttackCheck.position.y,counterAttackCheck.position.z),checkCounterAttackRadius);
        }

        public virtual void SetVelocity(float _velocityX,float _velocityY)
        {
            if (!isKnockback)
            {
                rb.velocity = new Vector2(_velocityX, _velocityY);
            }
        }

        public virtual void ZeroVelocity()
        {
            if (!isKnockback)
            {
                rb.velocity = Vector3.zero;
                
            }
        }
        /// <summary>
        /// 受伤者 受伤
        /// </summary>
        /// <param name="enemy">攻击源</param>
        /// <param name="enemyWeaponDamage">攻击源的武器伤害</param>
        /// <param name="isFrozenTime">当前的受伤者是否处于</param>
        /// <param name="enemyStats">打出伤害的对象的属性</param>
        /// <param name="damageType">伤害类型</param>
        /// <param name="type">异常类型</param>

        public virtual void TakeDamage(GameObject enemy,float enemyWeaponDamage,bool isFrozenTime,CharacterStats enemyStats,DamageType damageType,AilmentType type, out bool isCriti)
        {
            if (stats.IsEvasion() || !stats.canBeHurt)
            {
                isCriti = false;
                return;
            }
            stats.MinusHealth(enemyWeaponDamage,enemyStats,out var isCritical,damageType,type); // 敌人的武器伤害 加上敌人属性加成的伤害传入计算
            TakeDamageEffect(enemy, isFrozenTime,isCritical,enemyStats);
            isCriti = isCritical;
        }

        protected virtual void TakeDamageEffect(GameObject enemy, bool isFrozenTime,bool isCritical,CharacterStats enemyStats)
        {
            if (!isFrozenTime && canBeKnocked)
            {
                StartCoroutine(Knockback(enemy,isCritical));
            }
            if (isCritical)
            {
                enemyStats.GetComponent<EntityFX>().GenerateCriticalHitFX(enemy,gameObject);
            }
            fx.StartCoroutine("FlashHitFX");
        }

        private IEnumerator Knockback(GameObject enemy,bool isCritical)
        {
            isKnockback = true;
            int dir = 0;
            if (enemy.transform.position.x - transform.position.x > 0)
            {
                dir = -1;
            }
            else if(enemy.transform.position.x - transform.position.x < 0)
            {
                dir = 1;
            }

            if (isCritical)
            {
                rb.velocity = new Vector2(dir * knockbackForce.x*1.5f, knockbackForce.y+1);
                yield return new WaitForSeconds(knockbackDura*1.5f);
            }
            else
            {
                rb.velocity = new Vector2(dir * knockbackForce.x, knockbackForce.y);
                yield return new WaitForSeconds(knockbackDura);
            }
            
            isKnockback = false;
        }

        public virtual void MakeTransparent(bool isTransparent)
        {
            if (isTransparent)
            {
                sr.color=Color.clear;
            }
            else
            {
                sr.color = Color.white;
            }
        }

        public virtual void Die()
        {
            // subclass do something
        }

        public virtual void OnStatsChanged()
        {
            
        }
    }
}
