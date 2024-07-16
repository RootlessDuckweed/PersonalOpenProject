
using Player.Skill;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationEvent : MonoBehaviour
    {
        private PlayerController _player;

        private void Awake()
        {
            _player = GetComponentInParent<PlayerController>();
        }

        public void AnimationFinishTrigger()
        {
            _player.AnimationFinishTrigger();
        }

        public void ThrowSword()
        {
            SkillManager.Instance.swordSkill.CanUseSkill();
        }
    }
}
