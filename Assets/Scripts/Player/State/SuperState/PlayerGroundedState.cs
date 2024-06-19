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
        }
        
        private void Jump(InputAction.CallbackContext obj)
        { 
            if (player.CheckGround())
            {
                stateMachine.ChangeState(player.jumpState);
            }
                
        }
    }
}