using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] public  Animator anim { get; private set; }
        [SerializeField] public Rigidbody2D rb { get; private set; }
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
            if (wallCheck == null)
            {
                wallCheck = transform;
            }
        }

        protected virtual void Update()
        {
            //CheckPhysics();
        }
        
        protected virtual void Flip()
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

    }
}
