using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Entity : MonoBehaviour
    {
        public  Animator anim { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public EntityFX fx { get; private set; }

        [Header("Check whether on the Ground")]
        [SerializeField] protected LayerMask whatIsGround;
        [SerializeField] protected float checkGroundRadius;
        [SerializeField] protected Transform groundCheck;
        
        [Header("Flip(need to set by manual)")]
        public bool isFacingRight; // need to set in inspector
        public int facingDir;

        [Header("Check Wall")] 
        [SerializeField] protected float checkWallRadius;
        [SerializeField] protected Transform wallCheck;
        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
            fx = GetComponent<EntityFX>();
            
            if (wallCheck == null)
            {
                wallCheck = transform;
            }
        }
        
        protected virtual void Update()
        {
            //CheckPhysics();
        }
        
        public virtual void Flip()
        {
            isFacingRight = !isFacingRight;
            if (isFacingRight) 
                facingDir = 1;
            else
                facingDir = -1;
            
            transform.Rotate(0,180,0);
        }
        
       

        public virtual bool CheckGround()
        {
            return  Physics2D.Raycast(groundCheck.position, Vector2.down, checkGroundRadius,whatIsGround);
            
        }

        public virtual bool CheckWall()
        {
           return Physics2D.Raycast(wallCheck.position, Vector2.right*facingDir, checkWallRadius,whatIsGround);
           
        }
        
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,groundCheck.position.y-checkGroundRadius));
            Gizmos.DrawLine(wallCheck.position,new Vector3(wallCheck.position.x+checkWallRadius*facingDir,wallCheck.position.y));
        }

        public void SetVelocity(float _velocityX,float _velocityY)
        {
            rb.velocity = new Vector2(_velocityX, _velocityY);
        }

        public void ZeroVelocity()
        {
            rb.velocity = Vector3.zero;
        }

        public virtual void TakeDamage(GameObject enemy,float damage)
        {
            print("take damage : "+ damage+" from "+enemy);
            fx.StartCoroutine("FlashHitFX");
        }
    }
}
