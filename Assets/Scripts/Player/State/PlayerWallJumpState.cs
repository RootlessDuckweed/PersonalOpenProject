using Player.Universal;
using UnityEngine;

namespace Player.State
{
    public class PlayerWallJumpState :PlayerState
    {
        public PlayerWallJumpState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.rb.velocity = new Vector2(-player.facingDir*5, player.jumpForce);
            player.Flip();
            
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer > 0.2f)
            {
                stateMachine.ChangeState(player.airState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}