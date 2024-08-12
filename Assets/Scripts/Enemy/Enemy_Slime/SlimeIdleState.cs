using Enemy.Enemy_Slime.SuperState;
using UnityEngine;

namespace Enemy.Enemy_Slime
{
    public class SlimeIdleState : SlimeGroundState
    {
        public SlimeIdleState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer >= slimeEnemy.idleTime)
            {
                if (!slimeEnemy.CheckGround() || slimeEnemy.CheckWall())
                {
                    slimeEnemy.Flip(); 
                }
                stateMachine.ChangeState(slimeEnemy.moveState);
                Debug.Log("goto move");
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}