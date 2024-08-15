using Enemy.Enemy_BringerOfDeath.SuperState;
using UnityEngine;

namespace Enemy.Enemy_BringerOfDeath.State
{
    public class BringerTeleportState : BringerState
    {
        public BringerTeleportState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            bringerEnemy.stats.MakeInvincible(true);
            bringerEnemy.ZeroVelocity();
        }

        public override void Update()
        {
            base.Update();
            Debug.Log(triggerCalled);
            if (triggerCalled)
            {
                if (bringerEnemy.CanDoSpellCast())
                {
                    stateMachine.ChangeState(bringerEnemy.spellCastState);
                }
                else
                    stateMachine.ChangeState(bringerEnemy.battleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            bringerEnemy.stats.MakeInvincible(false);
            bringerEnemy.isTeleportToPlayer = false;
        }
    }
}