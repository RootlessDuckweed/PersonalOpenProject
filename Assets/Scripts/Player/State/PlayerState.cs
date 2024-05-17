using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.State
{
    public class PlayerState
    {
        protected PlayerController player;
        protected PlayerStateMachine stateMachine;
        protected string animBoolName;

        public PlayerState(PlayerController _player,PlayerStateMachine _stateMachine,string _animBoolName)
        {
            this.player = _player;
            this.stateMachine = _stateMachine;
            this.animBoolName = _animBoolName;
        }

        protected PlayerState()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Enter()
        {
            player.anim.SetBool(animBoolName,true);
        }

        public virtual void Update()
        {
            
        }

        public virtual void Exit()
        {
            player.anim.SetBool(animBoolName,false);
        }
    }
}
