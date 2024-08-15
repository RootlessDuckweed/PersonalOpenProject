namespace Enemy.State
{
    public class JumpBehindNoFallState : ArcherState
    {
        public JumpBehindNoFallState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
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
                stateMachine.ChangeState(archerEnemy.crouchAttackSate);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}