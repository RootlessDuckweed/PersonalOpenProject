using Enemy.Enemy_Archer.State;
using Enemy.State;
using Player.Universal;
using UnityEngine;

namespace Enemy
{
    public class ArcherEnemy : Enemy
    {
        //todo:死亡逻辑后面写
        public GameObject arrowPrefab;
        public float originalGravity;
        #region MyRegion
        public ArcherBattleState battleState { get; private set; }
        public AirAttackFallDownState attackFallDownState{ get; private set; }
        public ArcherAirContinueAttackState airContinueAttackState{ get; private set; }
        public ArcherAirCrouchAttackState airCrouchAttackState { get; private set; }
        public ArcherAirState airState{ get; private set; }
        public ArcherCrouchAttackSate crouchAttackSate{ get; private set; }
        public ArcherIdleState idleState{ get; private set; }
        public ArcherJumpBehindSate jumpBehindState{ get; private set; }
        public ArcherMoveState moveState{ get; private set; }
        public ArcherNormalAttackState normalAttackState{ get; private set; }
        public ArcherAttackBusyState attackBusyState{ get; private set; }
        public JumpBehindNoFallState jumpBehindNoFallState{ get; private set; }
        #endregion

        protected  void Start()
        {
            battleState = new ArcherBattleState(stateMachine, this, "Walk");
            attackFallDownState = new AirAttackFallDownState(stateMachine, this, "AirAttackFallDown");
            airContinueAttackState = new ArcherAirContinueAttackState(stateMachine, this, "AirAttackContinue");
            crouchAttackSate = new ArcherCrouchAttackSate(stateMachine, this, "CrouchAttack");
            airState = new ArcherAirState(stateMachine, this, "Air");
            airCrouchAttackState = new ArcherAirCrouchAttackState(stateMachine, this, "AirCrouchAttack");
            idleState = new ArcherIdleState(stateMachine, this, "Idle");
            jumpBehindState = new ArcherJumpBehindSate(stateMachine, this, "Jump");
            moveState = new ArcherMoveState(stateMachine, this, "Walk");
            normalAttackState = new ArcherNormalAttackState(stateMachine, this, "AttackNormal");
            attackBusyState = new ArcherAttackBusyState(stateMachine, this, "Idle");
            jumpBehindNoFallState = new JumpBehindNoFallState(stateMachine, this, "Air");
            stateMachine.Initialize(idleState);
            originalGravity = rb.gravityScale;
        }

        public void CreateArrow()
        {
           var newArrow= Instantiate(arrowPrefab,
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                Quaternion.identity);
           var dir = (PlayerManager.Instance.Player.transform.position - transform.position).normalized;
           newArrow.GetComponent<ArrowController>().SetupArrow(stats,dir);
        }

        public override RaycastHit2D IsDetectedPlayer()
        {
            return Physics2D.CircleCast(transform.position, attackDistance,Vector2.zero,0f,whatIsPlayer);
        }
        public float DistanceWithPlayer()
        {
            return Vector2.Distance(transform.position, PlayerManager.Instance.Player.transform.position);
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.red; // 可以更改颜色
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }

        public void FacingPlayer()
        {
            if (PlayerManager.Instance.Player.transform.position.x - transform.position.x > 1 && facingDir != 1)
            {
                Flip();
            }
            else if (PlayerManager.Instance.Player.transform.position.x - transform.position.x < -1 && facingDir != -1)
            {
                Flip();
            }
        }
    }
}