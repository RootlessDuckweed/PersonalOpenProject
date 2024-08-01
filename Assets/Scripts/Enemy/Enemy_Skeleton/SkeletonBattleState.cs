using Player;
using Player.Universal;
using UnityEngine;

namespace Enemy.Enemy_Skeleton
{
    public class SkeletonBattleState : EnemyState
    {
        private SkeletonEnemy _enemy;
        private readonly Transform _playerTrans;
        private float _battleTime;
        public SkeletonBattleState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is SkeletonEnemy)
            {
                _enemy = base.enemyBase as SkeletonEnemy;
            }

            _playerTrans = PlayerManager.Instance.Player.transform;
        }

        public override void Enter()
        {
            base.Enter();
            _battleTime = _enemy.battleTime ;
        }

        public override void Update()
        {
            base.Update();
            _battleTime-=Time.deltaTime;
            if (_enemy.IsDetectedPlayer())
            {
                if (_enemy.IsDetectedPlayer().distance <= _enemy.attackDistance)
                {
                    
                    stateMachine.ChangeState(_enemy.attackState);
                    
                }
                
            }
            else
            {
                  if (_playerTrans.position.x - _enemy.transform.position.x > 1 &&_enemy.facingDir!=1)
                  {
                      _enemy.Flip();
                  }
                  else if(_playerTrans.position.x - _enemy.transform.position.x < -1 &&_enemy.facingDir!=-1)
                  {
                      _enemy.Flip();
                  }
                  _enemy.SetVelocity(_enemy.attackMoveSpeed*_enemy.facingDir,_enemy.rb.velocity.y);
                            
            }
            
          
            if (_battleTime <0f || Vector2.Distance(_enemy.transform.position,_playerTrans.position)>7)
            {
                _enemy.ZeroVelocity();
                stateMachine.ChangeState(_enemy.idleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
        }
        
    }
}