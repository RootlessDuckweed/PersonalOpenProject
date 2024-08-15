using Enemy.Enemy_BringerOfDeath.SuperState;

namespace Enemy.Enemy_BringerOfDeath.State
{
    public class BringerMoveState : BringerGroundState
    {
        public BringerMoveState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            bringerEnemy.SetVelocity(bringerEnemy.moveSpeed*bringerEnemy.facingDir,bringerEnemy.rb.velocity.y);
            if (bringerEnemy.CheckWall()||!bringerEnemy.CheckGround())
            {
                stateMachine.ChangeState(bringerEnemy.idleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}