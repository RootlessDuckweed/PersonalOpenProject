using UnityEngine;

namespace Enemy.Enemy_Skeleton
{
    public class SkeletonAttackBusyState : EnemyState
    {
        private SkeletonEnemy _enemy;
        public SkeletonAttackBusyState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is SkeletonEnemy)
            {
                _enemy = base.enemyBase as SkeletonEnemy;
            }
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            _enemy.ZeroVelocity();
            if (CanAttack())
            {
                stateMachine.ChangeState(_enemy.battleState);
            }
            
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        private bool CanAttack()
        {
            if (Time.time >= _enemy.lastTimeAttacked + _enemy.attackCoolDown)
            {
                return true;
            }

            return false;
        }
    }
}