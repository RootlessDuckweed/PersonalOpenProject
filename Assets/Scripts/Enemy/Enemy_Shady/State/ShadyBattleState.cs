using UnityEngine;

namespace Enemy.Enemy_Shady.State.SuperState
{
    public class ShadyBattleState : ShadySate
    {
        private float _battleTime;
        public ShadyBattleState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _battleTime = shadyEnemy.battleTime ;
        }

        public override void Update()
        {
            base.Update();
            _battleTime -= Time.deltaTime;
            
            if (_playerTrans.position.x - shadyEnemy.transform.position.x > 1 && shadyEnemy.facingDir != 1)
            {
                shadyEnemy.Flip();
            }
            else if (_playerTrans.position.x - shadyEnemy.transform.position.x < -1 && shadyEnemy.facingDir != -1)
            {
                shadyEnemy.Flip();
            }
            
            shadyEnemy.SetVelocity(shadyEnemy.attackMoveSpeed * shadyEnemy.facingDir, shadyEnemy.rb.velocity.y);


            var hitDash = shadyEnemy.DistanceWithPlayer();
            if (hitDash<15)
            {
                if (hitDash <= shadyEnemy.dashAttackRadius && Random.Range(0,100)<70)
                {
                    if (hitDash >= shadyEnemy.attackDistance+1.5f)
                    {
                        stateMachine.ChangeState(shadyEnemy.spurtState);
                        Debug.Log(hitDash+ ":" + shadyEnemy.dashAttackRadius);
                    }
                    else
                    {
                        stateMachine.ChangeState(shadyEnemy.attackState);
                    }

                }
                else if(hitDash <= shadyEnemy.dashAttackRadius && CanEnterDash() || hitDash >= shadyEnemy.dashAttackRadius+2f)
                {
                    stateMachine.ChangeState(shadyEnemy.dashState);
                }

            }

            if (_battleTime < 0f || Vector2.Distance(shadyEnemy.transform.position, _playerTrans.position) > 15)
            {
                shadyEnemy.ZeroVelocity();
                stateMachine.ChangeState(shadyEnemy.idleState);
            }
        
                // Attack distance must be less than this 

            
            
        }

        public override void Exit()
        {
            base.Exit();
        }

        private bool CanEnterDash()
        {
            return Time.time > shadyEnemy.lastEnterDashTime + shadyEnemy.dashCooldown;
        }
    }
}