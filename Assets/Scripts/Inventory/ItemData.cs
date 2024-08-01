using System;
using UnityEngine;
using Utility;
using Utility.EnumType;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Item Data",menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        public ItemType itemType;
        public string itemName;
        public Sprite icon;

        [Range(0, 100)] 
        public int dropChance;
    }
}