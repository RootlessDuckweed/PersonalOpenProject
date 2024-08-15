using UnityEngine;

namespace Enemy.Enemy_Slime
{
    public class SlimeAttackBusyState: EnemyState
    {
        protected SlimeEnemy slimeEnemy;
        public SlimeAttackBusyState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
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
            if (CanAttack())
            {
                stateMachine.ChangeState(slimeEnemy.battleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }
        
        private bool CanAttack()
        {
            if (Time.time >= slimeEnemy.lastTimeAttacked + slimeEnemy.attackCoolDown)
            {
                return true;
            }

            return false;
        }
    }
}