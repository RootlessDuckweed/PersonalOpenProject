
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
    }
}
