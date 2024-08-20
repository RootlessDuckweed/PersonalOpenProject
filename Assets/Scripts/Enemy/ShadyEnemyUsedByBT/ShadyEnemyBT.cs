using Enemy.Enemy_Shady;
using Player.Universal;
using UnityEngine;

namespace Enemy.ShadyEnemyUsedByBT
{
    public class ShadyEnemyBT : Enemy
    {
         //todo:死亡逻辑后面写
        public GameObject shadyShootPrefab;
        public float dashAttackRadius;
        public Transform dashLimitedLeftDownPoint;
        public Transform dashLimitedRightUpPoint;
        public float generateShadowTimer;
        public float generateShadowDuration = 0.05f;
        public float lastEnterDashTime;
        public float dashCooldown=10;
        public int amounts = 5;
        public int currentAmounts = 0;
        public float originalGravity{ get; private set; }
        private string currentAnimationName;
        
        private void Start()
        {
            originalGravity = rb.gravityScale;
            currentAnimationName = "Idle";
        }
        

        protected override void Update()
        {
           
        }

        public void CreateShadyShoot()
        {
            var newShadyShoot= Instantiate(shadyShootPrefab,
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                Quaternion.identity);
            var dir = (PlayerManager.Instance.Player.transform.position - transform.position).normalized;
            newShadyShoot.GetComponent<ShadyShootController>().SetupShadyShoot(stats,dir);
        }

        public void FacingPlayer()
        {
            if (PlayerManager.Instance.Player.transform.position.x - transform.position.x > 1 && facingDir != 1)
            {
                Flip();
            }
            else if (PlayerManager.Instance.Player.transform.position.x - transform.position.x < -1 &&facingDir != -1)
            {
               Flip();
            }
            
        }
        
        public void GenerateShadow()
        {
            generateShadowTimer -= Time.deltaTime;
            if (generateShadowTimer <= 0)
            {
                generateShadowTimer = generateShadowDuration;
                fx.GetOnShadowPool();
            }
        }

        public override void SetVelocity(float _velocityX, float _velocityY)
        {
            if(isFrozenTime) return;
            base.SetVelocity(_velocityX, _velocityY);
        }

        public  Collider2D IsDetectedPlayerUsedByDashAttack()
        {
            Collider2D[] playersInRange = Physics2D.OverlapCircleAll(transform.position, attackDistance, whatIsPlayer);
            if (playersInRange.Length>=1)
            {
                return playersInRange[0];
            }
            else
            {
                return null;
            }
        }
        

        protected  void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.red; // 可以更改颜色
            Gizmos.DrawWireSphere(transform.position, dashAttackRadius);
        }

        public float DistanceWithPlayer()
        {
            return Vector2.Distance(transform.position, PlayerManager.Instance.Player.transform.position);
        }

        public void ChangeAnimation(string animationName)
        {
            anim.SetBool(currentAnimationName,false);
            anim.SetBool(animationName,true);
            currentAnimationName = animationName;
        }
    }
}
    
