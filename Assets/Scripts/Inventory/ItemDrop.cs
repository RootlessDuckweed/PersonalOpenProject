using System.Collections.Generic;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Inventory
{
    public class ItemDrop : MonoBehaviour
    {
        [SerializeField] private GameObject dropPrefab;
        [SerializeField] private ItemData itemData;
        [SerializeField] private ItemData[] possibleDrop;
        [SerializeField] private int amountOfItems;
        private List<ItemData> dropList = new List<ItemData>();

        public virtual void GenerateDrop()
        {
            for (int i = 0; i < possibleDrop.Length; i++)
            {
                if (Random.Range(0, 100) < possibleDrop[i].dropChance)
                {
                    dropList.Add(possibleDrop[i]);
                }
            }

            if (dropList.Count <= 0)
            {
                return;
            }
            
            for (int i = 0; i < amountOfItems; i++)
            {
                if(dropList.Count <= 0) return;
                itemData = dropList[Random.Range(0, Mathf.Clamp(dropList.Count-1,1,dropList.Count))];
                dropList.Remove(itemData);
                DropItem(itemData);
            }
        }
        
        protected void DropItem(ItemData _itemData)
        {
            GameObject newDrop = Instantiate(dropPrefab, transform.position, quaternion.identity);
            Vector2 velocity = new Vector2(Random.Range(-2, 2), Random.Range(5, 8));
            newDrop.GetComponent<ItemObject>().SetupItem(_itemData,velocity);
        }
    }
}