using System;
using Inventory;
using UnityEngine;
using Utility;
using Utility.EnumType;

namespace Player.Universal
{
    public class PlayerAttack : Attack_Base
    {
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (stat == null)
            {
                stat = PlayerManager.Instance.Player.stats;
            } 
            if(other.gameObject.layer==LayerMask.NameToLayer("Enemy"))
                InventoryManager.Instance.GetEquipment(EquipmentType.Weapon)?.ExecuteItemSpecialEffect(PlayerManager.Instance.Player.gameObject,other.gameObject);
            
            base.OnTriggerEnter2D(other);
        }
        
    }
}