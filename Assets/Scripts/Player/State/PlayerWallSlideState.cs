using Player.Universal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.State
{
    
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.playerInput.GamePlay.Jump.started += ChangeState;
        }

        private void ChangeState(InputAction.CallbackContext obj)
        {
            stateMachine.ChangeState(player.wallJumpState);
        }

        public override void Update()
        {
            base.Update();
            
            
            if (player.inputDir.x != 0)
            {
                stateMachine.ChangeState(player.idleState);
            }

            if (stateMachine.currentState == player.wallSlideState)
            {
                if (player.inputDir.y < 0)
                {
                    player.rb.velocity = new Vector2(0, player.rb.velocity.y);
                }
                else
                {
                    player.rb.velocity = new Vector2(0, player.rb.velocity.y * .7f);
                }
            }

            if (player.CheckGround()||!player.CheckWall())
            {
                stateMachine.ChangeState(player.idleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            player.playerInput.GamePlay.Jump.started -= ChangeState;
        }
    }
}
