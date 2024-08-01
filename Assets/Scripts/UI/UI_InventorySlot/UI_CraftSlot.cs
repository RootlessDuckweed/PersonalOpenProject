using UnityEngine.EventSystems;

namespace UI.UI_InventorySlot
{
    public class UI_CraftSlot : UI_ItemSlot
    {
        private void OnEnable()
        {
            UpdateInventoryItem(item);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}