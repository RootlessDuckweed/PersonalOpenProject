using Enemy.Enemy_Slime.SuperState;
using UnityEngine;

namespace Enemy.Enemy_Slime
{
    public class SlimeMoveState : SlimeGroundState
    {
        public SlimeMoveState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            slimeEnemy.SetVelocity(slimeEnemy.moveSpeed*slimeEnemy.facingDir,slimeEnemy.rb.velocity.y);
            if (slimeEnemy.CheckWall()||!slimeEnemy.CheckGround())
            {
                stateMachine.ChangeState(slimeEnemy.idleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}