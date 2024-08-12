using Player.Skill;
using Player.Skill.SpecificSkills;
using Player.Skill.SpecificSkills.SwordSkill;
using Player.Universal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.State.SuperState
{
    public class PlayerGroundedState: PlayerState
    {
        
        public PlayerGroundedState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
        {
           
        }

        public override void Enter()
        {
            base.Enter(); 
            player.playerInput.GamePlay.Jump.performed += Jump;
            player.playerInput.GamePlay.Attack.started += Attack;
            player.playerInput.GamePlay.CounterAttack.started += CounterAttack;
            //player.playerInput.GamePlay.AimSword.performed += (obj) => Debug.Log("aim sword is perform");
            player.playerInput.GamePlay.AimSword.started += AimSword;
            player.playerInput.GamePlay.BlackHoleSkill.started += UseBlackHoleSkill;
        }

        private void UseBlackHoleSkill(InputAction.CallbackContext obj)
        {
            if(!SkillManager.Instance.cloneSkill.blackHoleSkillUnlocked) return;
            var skill = SkillManager.Instance.blackHoleSkill;
            if(skill.canUse && skill.coolDownCompleted)
                stateMachine.ChangeState(player.blackHoleSkillState);
        }

        private void AimSword(InputAction.CallbackContext obj)
        {
            if (HasNoThrewSword() && SkillManager.Instance.swordSkill.swordUnlocked)
            {
                stateMachine.ChangeState(player.aimSwordState);
            }
                
        }

        private void CounterAttack(InputAction.CallbackContext obj)
        {
            if(SkillManager.Instance.parrySkill.parryUnlocked && SkillManager.Instance.parrySkill.CanUseSkill())
                stateMachine.ChangeState(player.counterAttackState);
        }

        private void Attack(InputAction.CallbackContext obj)
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        
        

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            player.playerInput.GamePlay.Jump.performed -= Jump;
            player.playerInput.GamePlay.Attack.started -= Attack;
            player.playerInput.GamePlay.CounterAttack.started -= CounterAttack;
            player.playerInput.GamePlay.AimSword.started -= AimSword;
            player.playerInput.GamePlay.BlackHoleSkill.started -= UseBlackHoleSkill;
            
        }
        
        private void Jump(InputAction.CallbackContext obj)
        { 
            if (player.CheckGround())
            {
                stateMachine.ChangeState(player.jumpState);
            }
                
        }

        private bool HasNoThrewSword()
        {
            if (!player.threwSword)
            {
                return true;
            }
            player.threwSword.GetComponent<SwordSkillController>().ReturnSword();
            return false;
        }
    }
}