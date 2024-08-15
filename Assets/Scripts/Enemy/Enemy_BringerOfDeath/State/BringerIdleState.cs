using Enemy.Enemy_BringerOfDeath.SuperState;

namespace Enemy.Enemy_BringerOfDeath.State
{
    public class BringerIdleState : BringerGroundState
    {
        public BringerIdleState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer >= bringerEnemy.idleTime)
            {
                if (!bringerEnemy.CheckGround() || bringerEnemy.CheckWall())
                {
                    bringerEnemy.Flip(); 
                }
                stateMachine.ChangeState(bringerEnemy.moveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}