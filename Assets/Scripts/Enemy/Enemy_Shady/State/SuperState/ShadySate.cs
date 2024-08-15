using Player.Universal;
using UnityEngine;

namespace Enemy.Enemy_Shady.State.SuperState
{
    public class ShadySate : EnemyState
    {
        protected ShadyEnemy shadyEnemy;
        protected Transform _playerTrans;
        public ShadySate(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is ShadyEnemy)
            {
                shadyEnemy = base.enemyBase as ShadyEnemy;
            }
            _playerTrans = PlayerManager.Instance.Player.transform;
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}