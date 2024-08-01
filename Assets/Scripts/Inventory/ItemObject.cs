using System;
using Player;
using UnityEngine;

namespace Inventory
{
    public class ItemObject : MonoBehaviour
    {
        [SerializeField] private ItemData itemData;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb=GetComponent<Rigidbody2D>();
        }
        

        public void SetupItem(ItemData itemData, Vector2 velocity)
        {
            this.itemData = itemData; 
            rb.velocity = velocity;
            
            GetComponent<SpriteRenderer>().sprite = itemData?.icon;
            gameObject.name = itemData?.itemName;
        }

        public void PickupItem()
        {
            InventoryManager.Instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}