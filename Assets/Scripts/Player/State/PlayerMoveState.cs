using Player.State.SuperState;
using Player.Universal;
using UnityEngine;

namespace Player.State
{
    public class PlayerMoveState:PlayerGroundedState
    {
        public PlayerMoveState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
          
        }
        
        public override void Enter()
        {
            base.Enter();
            
        }

        public override void Update()
        {
            base.Update();
            Movement();
            
            if (player.inputDir.x == 0)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else if (!player.CheckGround())
            {
                stateMachine.ChangeState(player.airState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            //player.rb.velocity = new Vector2(0,player.rb.velocity.y);
        }
        private void Movement()
        {
            player. rb.velocity = new Vector2(player.moveSpeed * player.inputDir.x, player.rb.velocity.y);

        }

    }
}