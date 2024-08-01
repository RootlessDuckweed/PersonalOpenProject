using Character;
using UnityEngine;
using Utility;

namespace Inventory.EquipmentItemEffect.IceAndFireEffect
{
    public class IceAndFireAttack : Attack_Base
    {
        private IceAndFireController iceAndFireController;
        private void Start()
        {
            iceAndFireController = GetComponentInParent<IceAndFireController>();
            stat = iceAndFireController.attackerStats;
            originalWeaponDamage += stat.fireDamage.GetValue() + stat.iceDamage.GetValue();
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