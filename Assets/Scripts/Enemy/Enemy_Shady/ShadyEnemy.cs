using System;
using Enemy.Enemy_Shady.State;
using Enemy.Enemy_Shady.State.SuperState;
using Player.Universal;
using UnityEngine;

namespace Enemy.Enemy_Shady
{
    public class ShadyEnemy : Enemy
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
        public ShadyIdleState idleState { get; private set; }
        public ShadyDashState dashState { get; private set; }
        public ShadyBattleState battleState { get; private set; }
        public ShadyMoveState moveState { get; private set; }
        public ShadyAttackState attackState{ get; private set; }
        public ShadyStunState stunState{ get; private set; }
        public ShadyAttackBusyState attackBusyState{ get; private set; }
        public ShadyDashAttackState dashAttackState{ get; private set; }
        public ShadySpurtState spurtState{ get; private set; }
        
        private void Start()
        {
            idleState = new ShadyIdleState(stateMachine, this, "Idle");
            dashState = new ShadyDashState(stateMachine, this, "Dash");
            battleState = new ShadyBattleState(stateMachine, this, "Move");
            moveState = new ShadyMoveState(stateMachine, this, "Move");
            attackState = new ShadyAttackState(stateMachine, this, "Attack");
            stunState = new ShadyStunState(stateMachine, this, "Stun");
            attackBusyState = new ShadyAttackBusyState(stateMachine, this, "Idle");
            dashAttackState = new ShadyDashAttackState(stateMachine, this, "Attack");
            spurtState = new ShadySpurtState(stateMachine, this, "Dash");
            originalGravity = rb.gravityScale;
            stateMachine.Initialize(idleState);
        }
        
        public override bool CanBeStunned()
        {
            if (base.CanBeStunned())
            {
                stateMachine.ChangeState(stunState);
                return true;
            }

            return false;
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
        

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.red; // 可以更改颜色
            Gizmos.DrawWireSphere(transform.position, dashAttackRadius);
        }

        public float DistanceWithPlayer()
        {
            return Vector2.Distance(transform.position, PlayerManager.Instance.Player.transform.position);
        }
    }
}