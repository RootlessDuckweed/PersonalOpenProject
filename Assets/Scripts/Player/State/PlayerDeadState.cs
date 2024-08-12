using Player.Universal;
using SceneManager;
using UnityEngine;

namespace Player.State
{
    public class PlayerDeadState : PlayerState
    {
        public PlayerDeadState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.playerInput.Disable();
            player.cd.enabled = false;
            player.rb.bodyType = RigidbodyType2D.Static;
            Object.FindFirstObjectByType<UI.UI>().DeadTextAppear();
        }

        public override void Update()
        {
            base.Update();
            player.ZeroVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            player.playerInput.Enable();
            player.cd.enabled = true;
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }
    }
}