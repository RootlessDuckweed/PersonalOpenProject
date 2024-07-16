using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.State
{
    public class PlayerCatchSwordState : PlayerState
    {
        private Transform sword;
        public PlayerCatchSwordState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            sword = player.threwSword.transform;
            if (player.transform.position.x > sword.position.x && player.facingDir == 1)
            {
                player.Flip();
            }
            else if(player.transform.position.x < sword.position.x && player.facingDir == -1)
            {
                player.Flip();
            }
            player.SetVelocity(-player.facingDir*player.swordReturnImpact,player.rb.velocity.y);
        }

        public override void Update()
        {
            base.Update();
           if (triggerCalled)
           {
               stateMachine.ChangeState(player.idleState);
           }
        }

        public override void Exit()
        {
            base.Exit();
            
        }
    }
}