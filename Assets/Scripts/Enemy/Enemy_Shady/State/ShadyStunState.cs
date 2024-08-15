namespace Enemy.Enemy_Shady.State.SuperState
{
    public class ShadyStunState : ShadySate
    {
        public ShadyStunState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
          
            
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer >= shadyEnemy.stunnedDuration)
            {
                stateMachine.ChangeState(shadyEnemy.idleState);
            }
        }

        public override void Exit()
        { 
            base.Exit();
           
        }
    }
}