using UnityEngine;

namespace Inventory
{
    public class PlayerItemDrop : ItemDrop
    {
        [Range(0, 100)] [SerializeField] private int dropChance; 
        public override void GenerateDrop()
        {
           
            var inventory = InventoryManager.Instance;
            for (int i = inventory.equipments.Count-1; i >= 0; i--)
            {
                if (Random.Range(0, 100) < dropChance)
                {
                    DropItem(inventory.equipments[i].itemData);
                    inventory.UnequipItem(inventory.equipments[i].itemData as ItemDataEquipment,false);
                }
            }
            
            
        }
    }
}