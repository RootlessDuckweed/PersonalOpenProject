using Player.Universal;
using UnityEngine;

namespace Player.State
{
    public class PlayerState
    {
        protected PlayerController player;
        protected PlayerStateMachine stateMachine;
        private string animBoolName;
        public float stateTimer;
        protected bool triggerCalled;
        public PlayerState(PlayerController _player,PlayerStateMachine _stateMachine,string _animBoolName)
        {
            this.player = _player;
            this.stateMachine = _stateMachine;
            this.animBoolName = _animBoolName;
            
        }

        protected PlayerState()
        {
        }

        public virtual void Enter()
        {
            player.anim.SetBool(animBoolName,true);
            stateTimer = 0;
            triggerCalled = false;
           
        }

        public virtual void Update()
        {
            stateTimer += Time.deltaTime;
        }

        public virtual void Exit()
        {
            player.anim.SetBool(animBoolName,false);
            stateTimer = 0;
        }

        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;
        }
    }
}
