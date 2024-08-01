using System;

namespace Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public ItemData itemData;
        public int stackSize;

        public InventoryItem(ItemData data)
        {
            itemData = data;
            AddStack();
        }

        public void AddStack() => stackSize++;
        public void RemoveStack() => stackSize--;
    }
}