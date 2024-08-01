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
        public int facingDir { get; private set; }

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

        public virtual void TakeDamage(GameObject enemy,float enemyWeaponDamage,bool isFrozenTime,CharacterStats enemyStats,DamageType damageType,AilmentType type)
        {
            if(enemyStats.IsEvasion()) return;
            TakeDamageEffect(enemy, isFrozenTime);
            var isCrit = false; 
            stats.MinusHealth(enemyWeaponDamage,enemyStats,out isCrit,damageType,type); // 敌人的武器伤害 加上敌人属性加成的伤害传入计算
        }

        protected virtual void TakeDamageEffect(GameObject enemy, bool isFrozenTime)
        {
            if (!isFrozenTime)
            {
                StartCoroutine(Knockback(enemy));
            }
            fx.StartCoroutine("FlashHitFX");
        }

        private IEnumerator Knockback(GameObject enemy)
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
            rb.velocity = new Vector2(dir * knockbackForce.x, knockbackForce.y);
            yield return new WaitForSeconds(knockbackDura);
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
    }
}
