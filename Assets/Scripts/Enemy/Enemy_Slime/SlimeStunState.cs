namespace Enemy.Enemy_Slime
{
    public class SlimeStunState : EnemyState
    {
        protected SlimeEnemy slimeEnemy;
        public SlimeStunState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is SlimeEnemy)
            {
                slimeEnemy = base.enemyBase as SlimeEnemy;
            }
        }

        public override void Enter()
        {
            base.Enter();
            slimeEnemy.fx.InvokeRepeating("RedColorBlink",0,.1f);
        }

        public override void Update()
        {
            base.Update();
            if (triggerCalled)
            {
                stateMachine.ChangeState(slimeEnemy.idleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            slimeEnemy.fx.Invoke("CancelColorBlink",0);
        }
    }
}