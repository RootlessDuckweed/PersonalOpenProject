using UnityEngine;

namespace Enemy
{
    public class EnemyAnimationEvent : MonoBehaviour
    {
        private Enemy _enemy;

        protected virtual void Awake()
        {
            _enemy = GetComponentInParent<Enemy>();
        }

        public virtual void AnimationFinishTrigger()
        {
            _enemy.AnimationFinishTrigger();
        }

        public virtual void OpenCounterAttackWindow()
        {
            _enemy.OpenCounterAttackWindow();
        }
        public virtual void CloseCounterAttackWindow()
        {
            _enemy.CloseCounterAttackWindow();
        }

        public virtual void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}