using UnityEngine;

namespace Player.State
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.isAir = true;
        }

        public override void Update()
        {
            base.Update();
            MoveInAir();
            if (player.CheckGround())
            {
                stateMachine.ChangeState(player.idleState);
            }
            else if (player.CheckWall()&&player.inputDir.x==0)
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            player.isAir = false;
        }
        private void MoveInAir()
        {
            player.rb.velocity = new Vector2(player.moveSpeed*player.inputDir.x,player.rb.velocity.y );
        }
    }
}