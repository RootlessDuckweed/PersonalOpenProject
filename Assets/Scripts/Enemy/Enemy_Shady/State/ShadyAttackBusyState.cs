using UnityEngine;

namespace Enemy.Enemy_Shady.State.SuperState
{
    public class ShadyAttackBusyState : ShadySate
    {
        public ShadyAttackBusyState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            shadyEnemy.ZeroVelocity();
        }

        public override void Update()
        {
            base.Update();
            
            if (CanAttack())
            {
                stateMachine.ChangeState(shadyEnemy.battleState);
            }
            
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        private bool CanAttack()
        {
            if (Time.time >= shadyEnemy.lastTimeAttacked + shadyEnemy.attackCoolDown)
            {
                return true;
            }

            return false;
        }
    }
}