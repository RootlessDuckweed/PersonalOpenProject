using UnityEngine.EventSystems;

namespace UI.UI_InventorySlot
{
    public class UI_CraftSlot : UI_ItemSlot
    {
        
        protected override void Awake()
        {
            base.Awake();
        }
        

        public override void OnPointerDown(PointerEventData eventData)
        {
            if(item!=null && item.itemData!=null)
                ui.craftWindowUI.ShowTheCraftItem(item);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
           
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
           
        }
    }
}