using UnityEngine;

namespace Enemy.Enemy_Shady.State.SuperState
{
    public class ShadyMoveState : ShadyGroundState
    {
        public ShadyMoveState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            shadyEnemy.SetVelocity(shadyEnemy.moveSpeed*shadyEnemy.facingDir,shadyEnemy.rb.velocity.y);
            if (shadyEnemy.CheckWall()||!shadyEnemy.CheckGround())
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