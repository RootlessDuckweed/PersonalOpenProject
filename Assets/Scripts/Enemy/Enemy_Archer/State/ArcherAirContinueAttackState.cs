using Enemy.State;
using UnityEngine;

namespace Enemy.Enemy_Archer.State
{
    public class ArcherAirContinueAttackState : ArcherState
    {
        private int shootingAmount = 4;
        private float lastTime;
        private int currentAmount = 0;
        public ArcherAirContinueAttackState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            archerEnemy.rb.gravityScale = 0;
        }

        public override void Update()
        {
            base.Update();
            archerEnemy.ZeroVelocity();
            if (Time.time - lastTime > 0.5f)
            {
                archerEnemy.FacingPlayer();
                lastTime = Time.time;
                archerEnemy.CreateArrow();
                currentAmount++;
                if (currentAmount <= shootingAmount)
                {
                    stateMachine.ChangeState(archerEnemy.airContinueAttackState);
                }
                else
                {
                    stateMachine.ChangeState(archerEnemy.airState);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            archerEnemy.rb.gravityScale = archerEnemy.originalGravity;
            if (currentAmount > shootingAmount)
            {
                currentAmount = 0;
            }
        }
    }
}