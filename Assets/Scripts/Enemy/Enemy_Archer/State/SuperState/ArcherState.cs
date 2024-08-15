namespace Enemy.State
{
    public class ArcherState : EnemyState
    {
        protected ArcherEnemy archerEnemy;
        
        public ArcherState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is ArcherEnemy)
            {
                archerEnemy = base.enemyBase as ArcherEnemy;
            }
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}