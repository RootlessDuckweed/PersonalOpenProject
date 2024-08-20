using Player.Universal;
using UnityEngine;

namespace Enemy.State
{
    public class ArcherBattleState : ArcherState
    {
        private float _battleTime;
        private Transform _playerTrans;
        public ArcherBattleState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _battleTime = archerEnemy.battleTime ;
            _playerTrans = PlayerManager.Instance.Player.transform;
        }

        public override void Update()
        {
            base.Update();
            _battleTime -= Time.deltaTime;
            
            if (_playerTrans.position.x - archerEnemy.transform.position.x > 1 && archerEnemy.facingDir != 1)
            {
                archerEnemy.Flip();
            }
            else if (_playerTrans.position.x - archerEnemy.transform.position.x < -1 && archerEnemy.facingDir != -1)
            {
                archerEnemy.Flip();
            }
            
            archerEnemy.SetVelocity(archerEnemy.attackMoveSpeed * archerEnemy.facingDir, archerEnemy.rb.velocity.y);
            
            var hit = archerEnemy.IsDetectedPlayer();
            if (hit)
            {
                if (archerEnemy.DistanceWithPlayer() < 4 && Random.Range(0,100)<50)
                {
                    stateMachine.ChangeState(archerEnemy.jumpBehindState);
                }

                else if (archerEnemy.DistanceWithPlayer() <= archerEnemy.attackDistance)
                {
                     if(Random.Range(0,100)<50)
                         stateMachine.ChangeState(archerEnemy.airCrouchAttackState);
                     else
                         stateMachine.ChangeState(archerEnemy.normalAttackState);
                }
                _battleTime = archerEnemy.battleTime ;
            }
            else
            {
                
                if (_battleTime < 0f || Vector2.Distance(archerEnemy.transform.position, _playerTrans.position) > 15)
                {
                    archerEnemy.ZeroVelocity();
                    stateMachine.ChangeState(archerEnemy.idleState);
                }
        
                // Attack distance must be less than this 

            }
            
        }
    }
}