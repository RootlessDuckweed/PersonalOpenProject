using UnityEngine;

namespace Enemy.Enemy_Skeleton
{
    public class SkeletonAnimationEvent : EnemyAnimationEvent
    {
        private SkeletonEnemy _enemySkeleton;

        protected override void Awake()
        {
            _enemySkeleton = GetComponentInParent<SkeletonEnemy>();
        }

        public override void AnimationFinishTrigger()
        {
            _enemySkeleton.AnimationFinishTrigger();
        }

        public override void OpenCounterAttackWindow()
        {
            _enemySkeleton.OpenCounterAttackWindow();
        }
        public override void CloseCounterAttackWindow()
        {
            _enemySkeleton.CloseCounterAttackWindow();
        }
    }
}