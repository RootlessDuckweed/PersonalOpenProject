using UnityEngine;

namespace Enemy.State
{
    public class ArcherAttackBusyState : ArcherState
    {
        public ArcherAttackBusyState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            archerEnemy.ZeroVelocity();
        }

        public override void Update()
        {
            base.Update();
            archerEnemy.ZeroVelocity();
            if (CanAttack())
            {
                stateMachine.ChangeState(archerEnemy.battleState);
            }
            
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        private bool CanAttack()
        {
            if (Time.time >= archerEnemy.lastTimeAttacked + archerEnemy.attackCoolDown)
            {
                return true;
            }

            return false;
        }
    }
}