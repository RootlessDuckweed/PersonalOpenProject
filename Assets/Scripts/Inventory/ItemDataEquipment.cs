using System.Collections.Generic;
using Character;
using Player;
using Player.Universal;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;
using Utility.EnumType;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Equipment Item Data",menuName = "Inventory/EquipmentItemData")]
    public class ItemDataEquipment : ItemData
    {
        public EquipmentType equipmentType;
        public ItemEffect[] itemEffects;
        public float itemCooldown;
        [Header("Major Stats")] 
        public float strength;
        public float agility;
        public float intelligence;
        public float vitality;

        [Header("Offensive Stats")] 
        public float damage;
        public float critChance;
        public float critPower;

        [Header("Defensive Stats")] 
        public float health;
        public float armor;
        public float evasion;
        public float magicResistance;

        [Header("Magic Stats")] 
        public float fireDamage;
        public float iceDamage;
        public float lightning;

        [Header("Craft requirements")] 
        public List<InventoryItem> craftingMaterials;
        
        public void AddModifiers()
        {
            PlayerStats playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();
            playerStats.strength.AddModifier(strength);
            playerStats.agility.AddModifier(agility);
            playerStats.intelligence.AddModifier(intelligence);
            playerStats.vitality.AddModifier(vitality);
            playerStats.damage.AddModifier(damage);
            playerStats.critChance.AddModifier(critChance);
            playerStats.critPower.AddModifier(critPower);
            playerStats.maxHealth.AddModifier(health);
            playerStats.armor.AddModifier(armor);
            playerStats.evasion.AddModifier(evasion);
            playerStats.magicalResistance.AddModifier(magicResistance);
            playerStats.fireDamage.AddModifier(fireDamage);
            playerStats.iceDamage.AddModifier(iceDamage);
            playerStats.lightningDamage.AddModifier(lightning);
        }

        public void RemoveModifiers()
        {
            PlayerStats playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();
            playerStats.strength.RemoveModifier(strength);
            playerStats.agility.RemoveModifier(agility);
            playerStats.intelligence.RemoveModifier(intelligence);
            playerStats.vitality.RemoveModifier(vitality);
            playerStats.damage.RemoveModifier(damage);
            playerStats.critChance.RemoveModifier(critChance);
            playerStats.critPower.RemoveModifier(critPower);
            playerStats.maxHealth.RemoveModifier(health);
            playerStats.armor.RemoveModifier(armor);
            playerStats.evasion.RemoveModifier(evasion);
            playerStats.magicalResistance.RemoveModifier(magicResistance);
            playerStats.fireDamage.RemoveModifier(fireDamage);
            playerStats.iceDamage.RemoveModifier(iceDamage);
            playerStats.lightningDamage.RemoveModifier(lightning);
        }

        public void ExecuteItemNormalEffect()
        {
            foreach (var t in itemEffects)
            {
                t.ExecuteNormalEffect();
            }
        }

        public void CancelItemNormalEffect()
        {
            foreach (var t in itemEffects)
            {
                t.CancelNormalEffect();
            }
        }

        public void ExecuteItemSpecialEffect(GameObject player=null,GameObject enemy=null)
        {
            foreach (var t in itemEffects)
            {
                t.ExecuteSpecialEffect(player,enemy);
            }
        }

        public void CancelItemSpecialEffect(GameObject player = null, GameObject enemy = null)
        {
            foreach (var t in itemEffects)
            {
                t.CancelSpecialEffect(player,enemy);
            }
        }
    }
}