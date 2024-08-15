using UnityEngine;

namespace Enemy.Enemy_Shady.State.SuperState
{
    public class ShadyIdleState : ShadyGroundState
    {
        public ShadyIdleState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer >= shadyEnemy.idleTime)
            {
                if (!shadyEnemy.CheckGround() || shadyEnemy.CheckWall())
                {
                    shadyEnemy.Flip(); 
                }
                stateMachine.ChangeState(shadyEnemy.moveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
        
    }
}