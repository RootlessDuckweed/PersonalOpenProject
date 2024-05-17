
using Player.State;
using UnityEngine;

using UnityEngine.InputSystem;

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

        [Header("State")]
        public PlayerStateMachine stateMachine { get; private set; }
        public PlayerIdleState idleState  { get; private set; }
        public PlayerMoveState moveState  { get; private set; }
        public PlayerJumpState jumpState { get; private set; }
        public PlayerDashState dashState { get; private set; }
        public PlayerAirState airState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            playerInput = new PlayerInputSettings();
           
            playerInput.GamePlay.Attack.performed += Attack;
            stateMachine = new PlayerStateMachine();
            idleState = new PlayerIdleState(this,this.stateMachine,"Idle");
            moveState = new PlayerMoveState(this, this.stateMachine, "Move");
            dashState = new PlayerDashState(this, this.stateMachine, "Dash");
            jumpState = new PlayerJumpState(this, this.stateMachine, "Jump");
            airState = new PlayerAirState(this, this.stateMachine, "Jump");
        }

        private void Start()
        {
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
        private void Attack(InputAction.CallbackContext obj)
        {
            if(!isDashing)
                anim.SetTrigger("attack");
            
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

       

        #region Animation Event

        public void AttackOver()
        {
            isAttacking = false;
        }

        public void AttackBegin()
        {
            isAttacking = true;
        }
        
        #endregion
        
        
    }
}

