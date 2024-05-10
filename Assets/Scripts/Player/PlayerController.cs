using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerInputSettings playerInput;
        public Rigidbody2D rb;
        public Animator anim;
        public float moveSpeed;
        public Vector2 inputDir;
        public float jumpForce;
        public float checkGroundRadius;
        
        public bool isMoving;
        public bool isFacingRight=true;
        [Header("Check whether on the Ground")]
        public bool isGrounded;
        [SerializeField] private LayerMask whatIsGround;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
            playerInput = new PlayerInputSettings();
            playerInput.GamePlay.Jump.performed += Jump;
        }

      

        private void OnEnable()
        {
            playerInput.GamePlay.Enable();
        }

        private void OnDisable()
        {
            playerInput.GamePlay.Disable();
        }

        private void Update()
        {
            inputDir=playerInput.GamePlay.Move.ReadValue<Vector2>();
            CheckPhysics();
            Movement();
            FlipController();
            AnimationController();
            
        }

        private void FixedUpdate()
        {
           
        }

        private void Movement()
        {
            rb.velocity = new Vector2(moveSpeed * inputDir.x, rb.velocity.y);
        }
         private void Jump(InputAction.CallbackContext obj)
         {
             if (isGrounded)
             {
                 rb.velocity = new Vector2(rb.velocity.x, jumpForce);
             }
                
         }
        private void AnimationController()
        {
            anim.SetBool("isMoving",isMoving=rb.velocity.x!=0);
           
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0,180,0);
        }

        private void FlipController()
        {
            if (inputDir.x > 0 && !isFacingRight)
                Flip();
            else if(inputDir.x<0&& isFacingRight)
                Flip();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,transform.position.y-checkGroundRadius));
        }

        private void CheckPhysics()
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, checkGroundRadius,whatIsGround);
            
        }
    }
}

