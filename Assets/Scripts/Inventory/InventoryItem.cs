using System;

namespace Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public ItemData itemData;
        public int stackSize=0;

        public InventoryItem(ItemData data)
        {
            itemData = data;
            AddStack();
        }
        public InventoryItem(ItemData data,int amount)
        {
            itemData = data;
            stackSize += amount;
        }

        public void AddStack() => stackSize++;
        public void RemoveStack() => stackSize--;
    }
}