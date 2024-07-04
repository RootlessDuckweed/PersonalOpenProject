using System;
using NUnit.Framework.Internal;

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
        #endregion
       
        
        protected override void Awake()
        {
            base.Awake();
            idleState = new SkeletonIdleState(this.stateMachine, this, "Idle");
            moveState = new SkeletonMoveState(this.stateMachine, this, "Move");
            battleState = new SkeletonBattleState(this.stateMachine, this, "Move");
            attackState = new SkeletonAttackState(this.stateMachine, this, "Attack");
            atkBusyState = new SkeletonAttackBusyState(this.stateMachine, this, "Idle");
        }
        
        
        
        private void Start()
        {
            stateMachine.Initialize(idleState);
        }

        protected override void Update()
        {
            base.Update();
        }
        
    }
}