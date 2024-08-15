using Enemy.Enemy_BringerOfDeath.SuperState;
using UnityEngine;

namespace Enemy.Enemy_BringerOfDeath.State
{
    public class BringerAttackBusyState : BringerState
    {
        public BringerAttackBusyState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            bringerEnemy.ZeroVelocity();
        }

        public override void Update()
        {
            base.Update();
            
            if (CanAttack())
            {
                stateMachine.ChangeState(bringerEnemy.battleState);
            }
            
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        private bool CanAttack()
        {
            if (Time.time >= bringerEnemy.lastTimeAttacked + bringerEnemy.attackCoolDown)
            {
                return true;
            }

            return false;
        }
    }
}