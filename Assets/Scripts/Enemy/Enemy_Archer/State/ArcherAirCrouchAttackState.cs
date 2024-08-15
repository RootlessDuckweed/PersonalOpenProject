using Enemy.State;
using UnityEngine;


namespace Enemy.Enemy_Archer.State
{
    public class ArcherAirCrouchAttackState : ArcherState
    {
        
        public ArcherAirCrouchAttackState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            archerEnemy.rb.velocity = new Vector2(0, 25);
            archerEnemy.isFrozenTime = true;
        }

        public override void Update()
        {
            base.Update();
            if (archerEnemy.rb.velocity.y < 0.5f)
            {
                archerEnemy.rb.gravityScale = 0;
                if (triggerCalled)
                {
                    archerEnemy.FacingPlayer();
                    archerEnemy.CreateArrow();
                    stateMachine.ChangeState(archerEnemy.airContinueAttackState);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            archerEnemy.isFrozenTime = false;
            //archerEnemy.rb.gravityScale = archerEnemy.originalGravity;
        }
    }
}