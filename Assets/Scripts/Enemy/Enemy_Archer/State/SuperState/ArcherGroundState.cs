using Player.Universal;
using UnityEngine;

namespace Enemy.State
{
    public class ArcherGroundState : ArcherState
    {
        private Transform _playerTrans;
        public ArcherGroundState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _playerTrans = PlayerManager.Instance.Player.transform;
        }

        public override void Update()
        {
            base.Update();
            if (archerEnemy.IsDetectedPlayer()|| Vector2.Distance(archerEnemy.transform.position,_playerTrans.position)<6)
            {
                stateMachine.ChangeState(archerEnemy.battleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
    