using Player.Universal;
using UnityEngine;

namespace Enemy.Enemy_Slime.SuperState
{
    public class SlimeGroundState : EnemyState
    {
        protected SlimeEnemy slimeEnemy;
        private Transform _playerTrans;
        public SlimeGroundState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is SlimeEnemy)
            {
                slimeEnemy = base.enemyBase as SlimeEnemy;
            }

            _playerTrans = PlayerManager.Instance.Player.transform;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if (slimeEnemy.IsDetectedPlayer()|| Vector2.Distance(slimeEnemy.transform.position,_playerTrans.position)<6)
            {
                stateMachine.ChangeState(slimeEnemy.battleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }
    }
}