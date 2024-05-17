using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Enemy
{
    public class EnemySkeleton : Entity
    {
        [Header("move info")] 
        public float moveSpeed;
        
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();
            if(!CheckGround()||CheckWall())
                Flip();
            rb.velocity = new Vector2(facingDir * moveSpeed, rb.velocity.y);
        }
    }
}