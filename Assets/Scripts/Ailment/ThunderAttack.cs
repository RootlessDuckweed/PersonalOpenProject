using Character;
using UnityEngine;
using Utility;

namespace Ailment
{
    public class ThunderAttack : Attack_Base
    {
        private ThunderStrikeController thunderStrikeController;
        private void Awake()
        {
            thunderStrikeController = GetComponentInParent<ThunderStrikeController>();
            stat = thunderStrikeController.attacker;
            originalWeaponDamage += thunderStrikeController.damage;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            var isPlayer = stat is PlayerStats;
            var isEnemy = stat is EnemyStats;
            if(isPlayer && other.gameObject.layer==LayerMask.NameToLayer("Player")) return;
            if(isEnemy && other.gameObject.layer==LayerMask.NameToLayer("Enemy")) return;
            base.OnTriggerEnter2D(other);
        }
    }
}