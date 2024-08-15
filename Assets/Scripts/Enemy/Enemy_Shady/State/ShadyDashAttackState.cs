using Enemy.Enemy_Shady.State.SuperState;
using UnityEngine;

namespace Enemy.Enemy_Shady.State
{
    public class ShadyDashAttackState : ShadySate
    {
        public ShadyDashAttackState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            shadyEnemy.ZeroVelocity();
            shadyEnemy.rb.gravityScale = 0;
        }

        public override void Update()
        {
            Debug.Log(triggerCalled);
            base.Update();
            if (triggerCalled)
            {
                Debug.Log("DashAttack");
                shadyEnemy.CreateShadyShoot();
                shadyEnemy.currentAmounts++;
                if (shadyEnemy.currentAmounts >= shadyEnemy.amounts)
                {
                    stateMachine.ChangeState(shadyEnemy.idleState);
                }
                else
                {
                    stateMachine.ChangeState(shadyEnemy.dashState);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            shadyEnemy.rb.gravityScale = shadyEnemy.originalGravity;
            if (shadyEnemy.currentAmounts >= shadyEnemy.amounts)
            {
                shadyEnemy.currentAmounts = 0;
                shadyEnemy. lastEnterDashTime = Time.time;
            }

            triggerCalled = false;
        }
    }
}