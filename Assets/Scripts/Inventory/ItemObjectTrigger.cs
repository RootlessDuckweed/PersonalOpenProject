using Player;
using Player.Universal;
using UnityEngine;

namespace Inventory
{
    public class ItemObjectTrigger : MonoBehaviour
    {
        private ItemObject itemObject => GetComponentInParent<ItemObject>();
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() != null)
            {
                itemObject.PickupItem();
            }
        }
    }
}