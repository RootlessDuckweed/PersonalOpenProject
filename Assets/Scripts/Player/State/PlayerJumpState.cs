
using Player.State.SuperState;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.State
{
    public class PlayerJumpState: PlayerState
    {
        public PlayerJumpState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.isJump = true;
            player.rb.velocity = new Vector2(player.moveSpeed*player.inputDir.x, player.jumpForce);
        }

        public override void Update()
        {
            base.Update();
            MoveInJump();
            if (player.rb.velocity.y < 0f)
            {
                stateMachine.ChangeState(player.airState);
            }
        }

        public override void Exit()
        {
            player.isJump = false;
            base.Exit();
            //player.rb.velocity = new Vector2(0,0);
        }
       

        private void MoveInJump()
        {
            player.rb.velocity = new Vector2(player.moveSpeed*player.inputDir.x,player.rb.velocity.y );
        }
        
        
    }
}