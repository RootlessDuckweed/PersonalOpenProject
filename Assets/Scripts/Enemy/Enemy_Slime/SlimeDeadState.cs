using UnityEngine;

namespace Enemy.Enemy_Slime
{
    public class SlimeDeadState : EnemyState
    {
        protected SlimeEnemy slimeEnemy;
        public SlimeDeadState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is SlimeEnemy)
            {
                slimeEnemy = base.enemyBase as SlimeEnemy;
            }
        }

        public override void Enter()
        {
            base.Enter();
           
        }

        public override void Update()
        {
            base.Update();
            if(triggerCalled)
                MonoBehaviour.Destroy(slimeEnemy.gameObject);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}