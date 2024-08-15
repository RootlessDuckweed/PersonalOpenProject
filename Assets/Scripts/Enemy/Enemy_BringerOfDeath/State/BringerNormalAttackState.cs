using Enemy.Enemy_BringerOfDeath.SuperState;
using UnityEngine;
using Utility;

namespace Enemy.Enemy_BringerOfDeath.State
{
    public class BringerNormalAttackState : BringerState
    {
        public BringerNormalAttackState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            bringerEnemy.ZeroVelocity();
            bringerEnemy.chanceToTeleport += 5;
        }

        public override void Update()
        {
            base.Update();
          
            if (triggerCalled)
            {
                if(bringerEnemy.CanTeleport())
                    stateMachine.ChangeState(bringerEnemy.teleportState);
                else
                {
                    stateMachine.ChangeState(bringerEnemy.attackBusyState);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            bringerEnemy.lastTimeAttacked = Time.time;
        }
    }
}