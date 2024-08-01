using Player.Skill;
using Player.Universal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.State
{
    public class PlayerDashState : PlayerState
    {
        public PlayerDashState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
            player.playerInput.GamePlay.Dash.started += Dash;
        }

        private void Dash(InputAction.CallbackContext obj)
        {
            if(!player.dashCold&&player.inputDir.x!=0)
                stateMachine.ChangeState(player.dashState);
        }

        public override void Enter()
        {
            base.Enter(); 
            player. playerInput.GamePlay.Dash.canceled += DashCancel;
            SkillManager.Instance.cloneSkill.CreateCloneOnDashStart();
        }

        public override void Update()
        {
            base.Update();
            DashContinueTimeCounter();
            if(!player.isDashing&&player.CheckGround())
            {
                stateMachine.ChangeState(player.idleState);
            }
            else if (!player.isDashing && !player.CheckGround())
            {
                stateMachine.ChangeState(player.airState);
                
            }
            else
            {
                player. rb.velocity = new Vector2(player.dashSpeed * player.inputDir.x, 0);
            }
          
        }

        public override void Exit()
        {
            base.Exit();
            player. playerInput.GamePlay.Dash.canceled -= DashCancel;
            player. rb.velocity = new Vector2(0, player.rb.velocity.y);
            player. dashColdTimeCounter = player.dashColdTime;
            player.dashContinueTimeCounter = player.dashContinueTime;
            player. dashCold = true; 
            player.isDashing = false;
            SkillManager.Instance.cloneSkill.CreateCloneOnDashOver();
        }
        private void DashCancel(InputAction.CallbackContext obj)
        {
            if(player.CheckGround())
            {
                stateMachine.ChangeState(player.idleState);
            }
            else 
            {
                stateMachine.ChangeState(player.airState);
            }
        }

        private void DashContinueTimeCounter()
        {
            if (!player.dashCold)
            {
                if (Keyboard.current.leftShiftKey.isPressed)
                {
                    player.dashContinueTimeCounter -= Time.deltaTime;
                    player. isDashing = true;
                    if (player.dashContinueTimeCounter <=0)
                    {
                        player.isDashing = false;
                        player. dashCold = true;
                        player.dashContinueTimeCounter = player.dashContinueTime;
                        player. dashColdTimeCounter = player.dashColdTime;
                    }
                }
            }
            else
            {
                player.isDashing = false;
                Debug.Log("is dashcold");
            }
        }
    }
}