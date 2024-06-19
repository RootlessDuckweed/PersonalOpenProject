using UnityEngine;

namespace Enemy
{
    public class EnemyState
    {
        protected EnemyStateMachine stateMachine;
        protected Enemy enemyBase;
        protected bool triggerCalled;
        private string animBoolName;
        protected float stateTimer;
        public EnemyState(EnemyStateMachine _stateMachine,Enemy enemyBase,string _animBoolName)
        {
            this.stateMachine = _stateMachine;
            this.enemyBase = enemyBase;
            this.animBoolName = _animBoolName;
        }

        public virtual void Enter()
        {
            enemyBase.anim.SetBool(animBoolName,true);
            triggerCalled = false;
            stateTimer = 0;
        }

        public virtual void Update()
        {
            stateTimer += Time.deltaTime;
        }

        public virtual void Exit()
        {
            enemyBase.anim.SetBool(animBoolName,false);
        }
        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;
        }
    }
}