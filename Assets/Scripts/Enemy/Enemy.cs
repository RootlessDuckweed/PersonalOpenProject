using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] protected LayerMask whatIsPlayer;
        public float moveSpeed;
        public float idleTime;
        public EnemyStateMachine stateMachine { get; private set; }

        [Header("Attack Info")] 
        public float attackMoveSpeed;
        public float attackDistance;
        public float attackCoolDown;
        public float lastTimeAttacked;
        public float  battleTime;
        
        protected  override void Awake()
        {
            base.Awake();
            stateMachine = new EnemyStateMachine();
        }
        
        
        protected override void Update()
        {
            base.Update();
            stateMachine.currentState.Update();
            
        }

        #region Animation Event
        
        public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
        
        #endregion

        public virtual RaycastHit2D IsDetectedPlayer() =>
            Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, attackDistance, whatIsPlayer);

        protected override void OnDrawGizmosSelected()
        {   
            Gizmos.color = Color.red;
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+attackDistance*facingDir,wallCheck.position.y));
            Gizmos.color = Color.white;
            base.OnDrawGizmosSelected();
        }
    }
}