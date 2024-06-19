using UnityEngine;

namespace Enemy.Enemy_Skeleton
{
    public class SkeletonAttackState : EnemyState
    {
        private SkeletonEnemy _enemy;
        public SkeletonAttackState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
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

            if (triggerCalled)
            {
                stateMachine.ChangeState(_enemy.battleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _enemy.lastTimeAttacked = Time.time;
        }
    }
}