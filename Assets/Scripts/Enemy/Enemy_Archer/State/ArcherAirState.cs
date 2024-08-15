namespace Enemy.State
{
    public class ArcherAirState : ArcherState
    {
        public ArcherAirState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if (archerEnemy.CheckGround())
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