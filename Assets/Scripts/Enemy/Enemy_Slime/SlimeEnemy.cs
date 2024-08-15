using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Enemy_Slime
{
    public enum SlimeType
    {
        Big,
        Medium,
        Small
    }
    public class SlimeEnemy : Enemy
    {
        [Header("Slime Specific")]
        [SerializeField] private SlimeType slimeType;
        [SerializeField] private int amountOfSlimesToCreate;
        [SerializeField] private GameObject slimePrefab;
        [SerializeField] private Vector2 maxCreationVelocity;
        [SerializeField] private Vector2 minCreationVelocity;
        [SerializeField] private GameObject healthBar;
        #region states
        public SlimeAttackState attackState { get; private set; }
        public SlimeAttackBusyState attackBusyState { get; private set; }
        public SlimeBattleState battleState{ get; private set; }
        public SlimeIdleState idleState{ get; private set; }
        public SlimeMoveState moveState{ get; private set; }
        public SlimeDeadState deadState { get; private set;}
        public SlimeStunState stunState{ get; private set;}

        #endregion

        private float respawnTime;
        private bool isExecute;
        protected override void Awake()
        {
            base.Awake();
            respawnTime = Time.time;
            stats.MakeInvincible(true);
            cd.enabled = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            sr.color =Color.white;
            healthBar.SetActive(true);
        }

        private void Start()
        {
            attackState = new SlimeAttackState(stateMachine, this, "Attack");
            attackBusyState = new SlimeAttackBusyState(stateMachine, this, "Idle");
            battleState = new SlimeBattleState(stateMachine, this, "Move");
            idleState = new SlimeIdleState(stateMachine, this, "Idle");
            moveState = new SlimeMoveState(stateMachine, this, "Move");
            deadState = new SlimeDeadState(stateMachine, this, "Die");
            stunState = new SlimeStunState(stateMachine, this, "Stun");
            stateMachine.Initialize(idleState);
        }

        private void OnEnable()
        {
            
        }

        protected override void Update()
        {
            base.Update();
            if (!isExecute&&Time.time - respawnTime > 1.5f)
            {
                stats.MakeInvincible(false);
                isExecute = true;
            }
        }

        public override void SetVelocity(float _velocityX, float _velocityY)
        {
            if(isFrozenTime) return;
            base.SetVelocity(_velocityX, _velocityY);
        }

        public override bool CanBeStunned()
        {
            if (base.CanBeStunned())
            {
                stateMachine.ChangeState(stunState);
                return true;
            }

            return false;
        }

        public override void Die()
        {
            base.Die();
            stateMachine.ChangeState(deadState);
            if (slimeType == SlimeType.Small)
            {
                return;
            } 
            Invoke(nameof(CreateSlimes),1f);
        }

        private void CreateSlimes()
        {
            CreateSlimes(amountOfSlimesToCreate);
        }
        private void CreateSlimes(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var newSlime = Instantiate(slimePrefab, transform.position, quaternion.identity);
                var slimeEnemy = newSlime.GetComponent<SlimeEnemy>();
                slimeEnemy.stats.MakeInvincibleAndCancelWithTimeFor(1.5f);
                slimeEnemy.SetupSlime();
                slimeEnemy.slimeType =slimeType + 1;
                slimeEnemy.stats.maxHealth.AddModifier(-stats.maxHealth.GetValue()*0.3f);
                slimeEnemy.attackDistance *= 0.6f;
                slimeEnemy.stats.damage.AddModifier(-stats.damage.GetValue()*0.3f);
                slimeEnemy.gameObject.transform.localScale *= 0.6f;
            }
        }

        public void SetupSlime()
        {
            float x = Random.Range(minCreationVelocity.x, maxCreationVelocity.x);
            float y =  Random.Range(minCreationVelocity.y, maxCreationVelocity.y);
            isKnockback = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
            Invoke(nameof(CancelKnockBack),1.5f);
        }

        public void CancelKnockBack()
        {
            isKnockback = false;
        }

        public override void Flip()
        {
            isFacingRight = !isFacingRight;
            if (isFacingRight)
            {
                facingDir = 1;
            }
            else
            {
                facingDir = -1;
                
            }

            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            onFlipped?.Invoke();
        }
        
        
    }
}