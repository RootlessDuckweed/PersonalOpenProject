namespace Enemy.State
{
    public class ArcherIdleState : ArcherGroundState
    {
        public ArcherIdleState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer >= archerEnemy.idleTime)
            {
                if (!archerEnemy.CheckGround() || archerEnemy.CheckWall())
                {
                    archerEnemy.Flip(); 
                }
                stateMachine.ChangeState(archerEnemy.moveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}