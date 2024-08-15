namespace Enemy.State
{
    public class ArcherCrouchAttackSate : ArcherState
    {
        public ArcherCrouchAttackSate(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            archerEnemy.ZeroVelocity();
        }

        public override void Update()
        {
            base.Update();
            if (triggerCalled)
            {
                archerEnemy.FacingPlayer();
                archerEnemy.CreateArrow();
                stateMachine.ChangeState(archerEnemy.idleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}