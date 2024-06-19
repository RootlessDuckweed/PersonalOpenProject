using UnityEngine;

namespace Enemy.Enemy_Skeleton.SuperState
{
    public class SkeletonGroundState : EnemyState
    {
        protected  SkeletonEnemy _enemy;
        private Transform _playerTrans;
        public SkeletonGroundState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is SkeletonEnemy)
            {
                _enemy = base.enemyBase as SkeletonEnemy;
            }

            _playerTrans = GameObject.Find("Player").transform;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if (_enemy.IsDetectedPlayer()|| Vector2.Distance(_enemy.transform.position,_playerTrans.position)<2)
            {
                stateMachine.ChangeState(_enemy.battleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}