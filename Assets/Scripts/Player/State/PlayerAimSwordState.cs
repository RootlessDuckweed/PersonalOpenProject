using Player.Skill;
using Player.Universal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.State
{
    public class PlayerAimSwordState : PlayerState
    {
        public PlayerAimSwordState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            SkillManager.Instance.swordSkill.DotsActive(true);
            player.playerInput.GamePlay.AimSword.canceled += ReleaseAimSword;
        }

        private void ReleaseAimSword(InputAction.CallbackContext obj)
        {
            stateMachine.ChangeState(player.idleState);
        }

        public override void Update()
        {
            base.Update();
            player.ZeroVelocity();
            
        }

        public override void Exit()
        {
            base.Exit();
            SkillManager.Instance.swordSkill.DotsActive(false);
            player.playerInput.GamePlay.AimSword.canceled -= ReleaseAimSword;
        }
    }
}