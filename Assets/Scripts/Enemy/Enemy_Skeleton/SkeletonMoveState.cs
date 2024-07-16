using System.Drawing.Printing;
using Enemy.Enemy_Skeleton.SuperState;
using UnityEngine;

namespace Enemy.Enemy_Skeleton
{
    public class SkeletonMoveState : SkeletonGroundState
    {
        public SkeletonMoveState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            _enemy.SetVelocity(_enemy.moveSpeed*_enemy.facingDir,_enemy.rb.velocity.y);
            if (_enemy.CheckWall()||!_enemy.CheckGround())
            {
                stateMachine.ChangeState(_enemy.idleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}