using UnityEngine;

namespace Enemy.State
{
    public class ArcherNormalAttackState : ArcherState
    {
        public ArcherNormalAttackState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            archerEnemy.ZeroVelocity();
        }

        public override void Update()
        {
            base.Update();
            if (triggerCalled)
            {
                archerEnemy.CreateArrow();
                stateMachine.ChangeState(archerEnemy.attackBusyState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            archerEnemy.lastTimeAttacked = Time.time;
        }
    }
}