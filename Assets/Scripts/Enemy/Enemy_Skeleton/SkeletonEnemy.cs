namespace Enemy.Enemy_Skeleton
{
    public class SkeletonEnemy: Enemy
    {
        #region State

        public SkeletonIdleState idleState { get; private set; }
        public SkeletonMoveState moveState { get; private set; }
        public SkeletonBattleState battleState { get; private set; }
        public SkeletonAttackState attackState { get; private set; }
        public SkeletonAttackBusyState atkBusyState { get; private set; }
        public SkeletonStunnedState stunnedState { get; private set; }
        public SkeletonDeadState deadState{get; private set;}
        #endregion
       
        
        protected override void Awake()
        {
            base.Awake();
        }
        
        private void Start()
        {
            idleState = new SkeletonIdleState(this.stateMachine, this, "Idle");
            moveState = new SkeletonMoveState(this.stateMachine, this, "Move");
            battleState = new SkeletonBattleState(this.stateMachine, this, "Move");
            attackState = new SkeletonAttackState(this.stateMachine, this, "Attack");
            atkBusyState = new SkeletonAttackBusyState(this.stateMachine, this, "Idle");
            stunnedState = new SkeletonStunnedState(this.stateMachine, this, "Stunned");
            deadState =  new SkeletonDeadState(this.stateMachine, this, "Die");
            stateMachine.Initialize(idleState);
        }

        protected override void Update()
        {
            base.Update();
            
        }

        //override this method. if others places excute CanBeStunned method 
        //it will excute subclass this method of subclass
        public override bool CanBeStunned()
        {
            if (base.CanBeStunned())
            {
               stateMachine.ChangeState(stunnedState);
               return true;
            }

            return false;
        }

        public override void SetVelocity(float _velocityX, float _velocityY)
        {
            if(FrozenTimeBeAttacked) return;
            base.SetVelocity(_velocityX, _velocityY);
        }

        public override void Die()
        {
            base.Die();
            stateMachine.ChangeState(deadState);
        }
    }
}