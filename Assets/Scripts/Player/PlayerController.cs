
using Player.State;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Utility;

namespace Player
{
    public class PlayerController : Entity
    {
        public PlayerInputSettings playerInput;
        public float moveSpeed;
        public Vector2 inputDir;
        public float jumpForce;
        public bool isJump;
        public bool isAir;
        
        [Header("Sword")]
        public GameObject threwSword;
        public float swordReturnImpact;
        
        [Header("Dash Info")] 
        public float dashColdTime;
        public float dashColdTimeCounter;
        public float dashContinueTime;
        public float dashContinueTimeCounter;
        public bool dashCold;
        public float dashSpeed;
        public bool isDashing;

        [Header("Attack Info")]
        public bool isAttacking;
        public float counterAttackDuration = .2f;

        [Header("State")]
        public PlayerStateMachine stateMachine { get; private set; }
        public PlayerIdleState idleState  { get; private set; }
        public PlayerMoveState moveState  { get; private set; }
        public PlayerJumpState jumpState { get; private set; }
        public PlayerDashState dashState { get; private set; }
        public PlayerAirState airState { get; private set; }
        public PlayerWallSlideState wallSlideState { get; private set; }
        public PlayerWallJumpState wallJumpState { get; private set; }
        public PlayerPrimaryAttackState primaryAttackState { get; private set; }
        public PlayerCounterState counterAttackState { get; private set; }
        public PlayerCatchSwordState catchSwordState { get; private set;}
        public PlayerAimSwordState aimSwordState { get; private set;}
        public PlayerBlackHoleSkillState blackHoleSkillState { get; private set;}
        protected override void Awake()
        {
            base.Awake();
            playerInput = new PlayerInputSettings();
           
            //playerInput.GamePlay.Attack.performed += Attack;
            
        }

        private void Start()
        {
            stateMachine = new PlayerStateMachine();
            idleState = new PlayerIdleState(this,this.stateMachine,"Idle");
            moveState = new PlayerMoveState(this, this.stateMachine, "Move");
            dashState = new PlayerDashState(this, this.stateMachine, "Dash");
            jumpState = new PlayerJumpState(this, this.stateMachine, "Jump");
            airState = new PlayerAirState(this, this.stateMachine, "Jump");
            wallSlideState = new PlayerWallSlideState(this, this.stateMachine, "WallSlide");
            wallJumpState = new PlayerWallJumpState(this, this.stateMachine, "Jump");
            primaryAttackState = new PlayerPrimaryAttackState(this, this.stateMachine, "Attack");
            counterAttackState = new PlayerCounterState(this, this.stateMachine, "CounterAttack");
            aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
            catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
            blackHoleSkillState = new PlayerBlackHoleSkillState(this, stateMachine, "Jump");
            
            stateMachine.Initialize(idleState);
        }

        private void OnEnable()
        {
            playerInput.GamePlay.Enable();

        }

        private void OnDisable()
        {
            playerInput.GamePlay.Disable();
        }

        protected override void Update()
        {
            base.Update();
            InputController();
            FlipController();
            AnimationController();
            DashTimeCounter();
          
            stateMachine.currentState.Update();
        }


        private void InputController()
        {
            inputDir = playerInput.GamePlay.Move.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {

        }
        
       
        private void AnimationController()
        {
            anim.SetFloat("yVelocity",rb.velocity.y);
           // anim.SetBool("isAttacking",isAttacking);
        }
        

        private void FlipController()
        {
            if (inputDir.x > 0 && !isFacingRight)
                Flip();
            else if(inputDir.x<0&& isFacingRight)
                Flip();
        }
        

        private void DashTimeCounter()
        {
            if (dashCold&&dashColdTimeCounter >0)
            {
                dashColdTimeCounter -= Time.deltaTime;
                isDashing = false;
                if (dashColdTimeCounter <= 0)
                {
                    dashCold = false;
                    dashContinueTimeCounter = dashContinueTime;
                    //dashTimeCounter = dashTimedura;
                }

            }
        }

        public void ThrowTheSword(GameObject newSword)
        {
            threwSword = newSword;
        }

        public void CatchTheSword()
        {
            stateMachine.ChangeState(catchSwordState);
            Destroy(threwSword.gameObject);
            
        }

        public void ExitBlackHoleState()
        {
            stateMachine.ChangeState(idleState);
        }
        
        #region Animation Event
        
        public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
        
        #endregion
        
        
    }
}

