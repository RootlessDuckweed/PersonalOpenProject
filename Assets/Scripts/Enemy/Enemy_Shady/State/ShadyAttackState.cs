using Enemy.Enemy_Shady.State.SuperState;
using UnityEngine;

namespace Enemy.Enemy_Shady.State
{
    public class ShadyAttackState :  ShadySate
    {
        public ShadyAttackState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            shadyEnemy.ZeroVelocity();
        }

        public override void Update()
        {
            Debug.Log("Attack");
            base.Update();
            shadyEnemy.FacingPlayer();
            if (triggerCalled)
            {
                if (Random.Range(0, 100) < 30)
                {
                    shadyEnemy.CreateShadyShoot();
                }
                stateMachine.ChangeState(shadyEnemy.attackBusyState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            shadyEnemy.lastTimeAttacked = Time.time;
        }
    }
}