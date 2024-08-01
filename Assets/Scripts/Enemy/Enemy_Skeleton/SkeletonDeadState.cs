using UnityEngine;

namespace Enemy.Enemy_Skeleton
{
    public class SkeletonDeadState : EnemyState
    {
        private SkeletonEnemy _enemy;
        public SkeletonDeadState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is SkeletonEnemy)
            {
                _enemy = base.enemyBase as SkeletonEnemy;
            }

        }

        public override void Enter()
        {
            base.Enter();
            _enemy.cd.enabled = false;
            _enemy.rb.bodyType = RigidbodyType2D.Static;
        }

        public override void Update()
        {
            base.Update();
            //_enemy.ZeroVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            _enemy.cd.enabled = true;
            
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }
    }
}