using Player.State.SuperState;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.State
{
    public class PlayerIdleState :PlayerGroundedState
    {
        public PlayerIdleState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //player.anim.SetBool(animBoolName,true);
        }

        public override void Update()
        {
            base.Update();
            player.ZeroVelocity();
            if (player.inputDir.x != 0)
            {
                stateMachine.ChangeState(player.moveState);
            }

            if (!player.CheckGround())
            {
                stateMachine.ChangeState(player.airState);
            }
            
        }

        public override void Exit()
        {
            base.Exit();
            //player.anim.SetBool(animBoolName,false);
        }
    }
}