using System.Drawing.Printing;
using Enemy.Enemy_Skeleton.SuperState;
using UnityEngine;

namespace Enemy.Enemy_Skeleton
{
    public class SkeletonIdleState : SkeletonGroundState
    {
        
        public SkeletonIdleState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer >= _enemy.idleTime)
            {
                if (!_enemy.CheckGround() || _enemy.CheckWall())
                {
                    _enemy.Flip(); 
                }
                stateMachine.ChangeState(_enemy.moveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}