using System;
using System.Collections;
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

        [Header("Stunned info")] 
        public float stunnedDuration;
        public Vector2 stunnedDirection;
        protected bool canBeStunned;
        [SerializeField] protected GameObject counterImage;
        [SerializeField]private float defaultMoveSpeed;
        public bool isFrozenTime;
        protected  override void Awake()
        {
            base.Awake();
            stateMachine = new EnemyStateMachine();
            defaultMoveSpeed = moveSpeed;
        }
        
        protected override void Update()
        {
            base.Update();
            stateMachine.currentState.Update();
        }
        

        public virtual void OpenCounterAttackWindow()
        {
            canBeStunned = true;
            counterImage.SetActive(true);
        }

        public virtual void CloseCounterAttackWindow()
        {
            canBeStunned = false;
            counterImage.SetActive(false);
        }

        public virtual void FreezeTime(bool timeFrozen)
        {
            if (timeFrozen)
            {
                isFrozenTime = true;
                moveSpeed = 0;
                anim.speed = 0;
            }
            else
            {
                isFrozenTime = false;
                moveSpeed = defaultMoveSpeed;
                anim.speed = 1;
            }
        }

        protected virtual IEnumerator FreezeTimeFor(float seconds)
        {
            FreezeTime(true);
            yield return new WaitForSeconds(seconds);
            FreezeTime(false);
        }
        
        public virtual bool CanBeStunned()
        {
            //if Can be Stunned , perform stunning(excuted by subclass) and close the window 
            if (canBeStunned)
            {
                CloseCounterAttackWindow();
                return true;
            }

            return false;
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