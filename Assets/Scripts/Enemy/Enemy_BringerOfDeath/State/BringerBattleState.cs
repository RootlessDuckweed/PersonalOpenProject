using Enemy.Enemy_BringerOfDeath.SuperState;
using UnityEngine;

namespace Enemy.Enemy_BringerOfDeath.State
{
    public class BringerBattleState : BringerState
    {
        private float _battleTime;
        public BringerBattleState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _battleTime = bringerEnemy.battleTime ;
        }

        public override void Update()
        {
            base.Update();
            _battleTime-=Time.deltaTime;
            if (bringerEnemy.IsDetectedPlayer())
            {
                if (bringerEnemy.IsDetectedPlayer().distance <= bringerEnemy.attackDistance)
                {
                    stateMachine.ChangeState(bringerEnemy.attackState);
                }
                
            }
            else if(bringerEnemy.DistanceWithPlayer() > 6 && bringerEnemy.isCooldown)
            {
                stateMachine.ChangeState(bringerEnemy.teleportState);
                bringerEnemy.isTeleportToPlayer = true;
            }
            else
            {
                if (_playerTrans.position.x - bringerEnemy.transform.position.x > 1 &&bringerEnemy.facingDir!=1)
                {
                    bringerEnemy.Flip();
                }
                else if(_playerTrans.position.x - bringerEnemy.transform.position.x < -1 &&bringerEnemy.facingDir!=-1)
                {
                    bringerEnemy.Flip();
                }
                bringerEnemy.SetVelocity(bringerEnemy.attackMoveSpeed*bringerEnemy.facingDir,bringerEnemy.rb.velocity.y);
                            
            }
            
          
            if (_battleTime <0f || Vector2.Distance(bringerEnemy.transform.position,_playerTrans.position)>50)
            {
                bringerEnemy.ZeroVelocity();
                stateMachine.ChangeState(bringerEnemy.idleState);
            }
            
        }

        public override void Exit()
        {
            base.Exit();
            
        }
        
    }
}