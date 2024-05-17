using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AnimationEvent : MonoBehaviour
    {
        private PlayerController _player;

        private void Awake()
        {
            _player = GetComponentInParent<PlayerController>();
        }

        private void AttackOver()
        {
            _player.AttackOver();
        }

        private void AttackBegin()
        {
            _player.AttackBegin();
        }
    }
}
