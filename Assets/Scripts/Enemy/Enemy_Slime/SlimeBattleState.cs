using Enemy.Enemy_Slime.SuperState;
using Player.Universal;
using UnityEngine;

namespace Enemy.Enemy_Slime
{
    public class SlimeBattleState : EnemyState
    {
        protected SlimeEnemy slimeEnemy;
        private  Transform _playerTrans;
        private float _battleTime;
        public SlimeBattleState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is SlimeEnemy)
            {
                slimeEnemy = base.enemyBase as SlimeEnemy;
            }
            
        }

        public override void Enter()
        {
            base.Enter();
            _battleTime = slimeEnemy.battleTime;
            _playerTrans = PlayerManager.Instance.Player.transform;
        }

        public override void Update()
        {
            base.Update();
            _battleTime -= Time.deltaTime;
            if (slimeEnemy.IsDetectedPlayer())
            {
                    if (slimeEnemy.IsDetectedPlayer().distance <= slimeEnemy.attackDistance)
                    {
                        stateMachine.ChangeState(slimeEnemy.attackState);
                    }
            }
            else
            {
                if (_playerTrans.position.x - slimeEnemy.transform.position.x > 1 && slimeEnemy.facingDir != 1)
                {
                    slimeEnemy.Flip();
                   
                }
                else if (_playerTrans.position.x - slimeEnemy.transform.position.x < -1 &&
                         slimeEnemy.facingDir != -1)
                {
                    slimeEnemy.Flip();
                }

                slimeEnemy.SetVelocity(slimeEnemy.attackMoveSpeed * slimeEnemy.facingDir, slimeEnemy.rb.velocity.y);

            }


            if (_battleTime < 0f || Vector2.Distance(slimeEnemy.transform.position, _playerTrans.position) > 7)
            {
                slimeEnemy.ZeroVelocity();
                stateMachine.ChangeState(slimeEnemy.idleState);
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