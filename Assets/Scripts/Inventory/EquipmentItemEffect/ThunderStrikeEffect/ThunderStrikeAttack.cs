using System;
using Character;
using UnityEngine;
using Utility;

namespace Inventory.EquipmentItemEffect.ThunderStrikeEffect
{
    public class ThunderStrikeAttack : Attack_Base
    {
        private ThunderBigStrikeController thunderBigStrikeController;
        private void Start()
        {
            thunderBigStrikeController = GetComponentInParent<ThunderBigStrikeController>();
            stat = thunderBigStrikeController.attackerStats;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            var isPlayer = stat is PlayerStats;
            var isEnemy = stat is EnemyStats;
            if(isPlayer && other.gameObject.layer==LayerMask.NameToLayer("Player")) return;
            if(isEnemy && other.gameObject.layer==LayerMask.NameToLayer("Enemy")) return;
            base.OnTriggerEnter2D(other);
            print("is Thunder Strike");
        }
    }
}