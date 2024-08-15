namespace Enemy.State
{
    public class ArcherMoveState : ArcherGroundState
    {
        public ArcherMoveState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            archerEnemy.SetVelocity(archerEnemy.moveSpeed*archerEnemy.facingDir,archerEnemy.rb.velocity.y);
            if (archerEnemy.CheckWall()||!archerEnemy.CheckGround())
            {
                stateMachine.ChangeState(archerEnemy.idleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}