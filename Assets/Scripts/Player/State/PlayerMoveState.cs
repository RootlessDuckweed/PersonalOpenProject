using Player.State.SuperState;
using UnityEngine;
using UnityEngine.InputSystem;

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
        }

        public override void Exit()
        {
            base.Exit();
            player.rb.velocity = new Vector2(0,player.rb.velocity.y);
        }
        private void Movement()
        {
            player. rb.velocity = new Vector2(player.moveSpeed * player.inputDir.x, player.rb.velocity.y);

        }

    }
}