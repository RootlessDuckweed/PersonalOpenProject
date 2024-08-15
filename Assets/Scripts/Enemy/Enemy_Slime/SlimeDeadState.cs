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
            slimeEnemy.cd.enabled = false;
            slimeEnemy.rb.bodyType = RigidbodyType2D.Static;
        }

        public override void Update()
        {
            base.Update();
            if(triggerCalled)
                MonoBehaviour.Destroy(slimeEnemy.gameObject,1.5f);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}