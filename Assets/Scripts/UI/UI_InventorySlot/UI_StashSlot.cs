using UnityEngine.EventSystems;

namespace UI.UI_InventorySlot
{
    public class UI_StashSlot : UI_ItemSlot
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if(item == null || item.itemData == null) return;
            ui.itemTooltip.ShowToolTip(item.itemData,ui.craftPanelItemToolTipPosition);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
        }
    }
}