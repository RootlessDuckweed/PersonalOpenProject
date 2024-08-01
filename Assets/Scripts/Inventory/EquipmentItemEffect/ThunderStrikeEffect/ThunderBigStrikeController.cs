using System;
using Character;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory.EquipmentItemEffect.ThunderStrikeEffect
{
    public class ThunderBigStrikeController : MonoBehaviour
    {
        public CharacterStats attackerStats;
        private Animator anim;
        private static readonly int Hit = Animator.StringToHash("Hit");

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
        }

        public void Setup(CharacterStats attacker,Transform target)
        {
            transform.position = target.position;
            anim.SetTrigger(Hit);
            attackerStats = attacker;
            DestroySelf();
        }

        private void DestroySelf()
        {
            Destroy(gameObject,0.5f);
        }
    }
}

