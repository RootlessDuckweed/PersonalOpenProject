using Inventory;
using Player.Universal;
using UnityEngine;
using Utility;
using Utility.EnumType;

namespace Player.Skill.SpecificSkills.CrystalSkill
{
    public class CrystalAttack : Attack_Base
    {
        private void Start()
        {
            stat = PlayerManager.Instance.Player.stats;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (InventoryManager.Instance.GetEquipment(EquipmentType.Amulet)?.itemName == "ThunderGodAmulet")
            {
                print("ThunderGodAmulet");
                InventoryManager.Instance.GetEquipment(EquipmentType.Amulet).ExecuteItemSpecialEffect(PlayerManager.Instance.Player.gameObject,other.gameObject);
            }
            base.OnTriggerEnter2D(other);
        }
    }
}