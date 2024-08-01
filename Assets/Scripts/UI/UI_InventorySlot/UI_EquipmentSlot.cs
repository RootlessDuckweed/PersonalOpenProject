using Inventory;
using UnityEngine.EventSystems;
using Utility.EnumType;

namespace UI.UI_InventorySlot
{
    public class UI_EquipmentSlot : UI_ItemSlot
    {
        public EquipmentType equipmentSlotType;

        
        public override void OnPointerDown(PointerEventData eventData)
        {
            if(item==null||item.itemData == null) return;
            InventoryManager.Instance.UnequipItem(item.itemData as ItemDataEquipment,true);
        }
    }
}