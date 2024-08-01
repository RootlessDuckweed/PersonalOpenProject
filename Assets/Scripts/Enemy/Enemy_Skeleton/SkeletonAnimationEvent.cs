using UnityEngine;

namespace Enemy.Enemy_Skeleton
{
    public class SkeletonAnimationEvent : MonoBehaviour
    {
        private SkeletonEnemy _enemy;

        private void Awake()
        {
            _enemy = GetComponentInParent<SkeletonEnemy>();
        }

        public void AnimationFinishTrigger()
        {
            _enemy.AnimationFinishTrigger();
        }

        public void OpenCounterAttackWindow()
        {
            _enemy.OpenCounterAttackWindow();
        }
        public void CloseCounterAttackWindow()
        {
            _enemy.CloseCounterAttackWindow();
        }
    }
}