using Player.Universal;
using UnityEngine;

namespace Enemy.Enemy_BringerOfDeath.SuperState
{
    public class BringerState : EnemyState
    {
        protected DeathBringerEnemy bringerEnemy;
        protected Transform _playerTrans;
        public BringerState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is DeathBringerEnemy)
            {
                bringerEnemy = base.enemyBase as DeathBringerEnemy;
            }

            _playerTrans = PlayerManager.Instance.Player.transform;
        }
        
    }
}