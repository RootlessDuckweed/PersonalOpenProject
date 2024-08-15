using Enemy.Enemy_Slime.SuperState;
using UnityEngine;

namespace Enemy.Enemy_Slime
{
    public class SlimeAttackState : EnemyState
    {
        private SlimeEnemy slimeEnemy;
        public SlimeAttackState(EnemyStateMachine _stateMachine, global::Enemy.Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is SlimeEnemy)
            {
                slimeEnemy = base.enemyBase as SlimeEnemy;
            }

        }

        public override void Enter()
        {
            base.Enter();
            slimeEnemy.ZeroVelocity();
        }

        public override void Update()
        {
            base.Update();
            if (triggerCalled)
            {
                stateMachine.ChangeState(slimeEnemy.attackBusyState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            slimeEnemy.lastTimeAttacked = Time.time;
        }
        
    }
}