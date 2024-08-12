using System;
using Inventory;
using Player.Universal;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utility.EnumType;

namespace UI.UI_InventorySlot
{
    public class UI_ItemSlot : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] protected Image itemImage;
        public TextMeshProUGUI itemText;

        public InventoryItem item;
        protected UI ui;

        protected virtual void Awake()
        {
            ui = GetComponentInParent<UI>();
        }

        public void UpdateInventoryItem(InventoryItem newItem)
        {
            item = newItem;
            if (item == null || item.itemData==null) return;
            itemImage.color = Color.white;
            itemImage.sprite = item.itemData.icon;
            if (item.stackSize >= 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
                Clear();
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if(item == null || item.itemData == null) return;

            if (Keyboard.current.leftCtrlKey.isPressed)
            {
                InventoryManager.Instance.RemoveItem(item.itemData);
                return;
            }
            
            if(item.itemData.itemType==ItemType.Material)
            {
                ui.craftWindowUI.AddToSelectingSlots(item);
                return;
            }
            
            var itemEquipment = item.itemData as ItemDataEquipment;
            if (itemEquipment.equipmentType == EquipmentType.Flask)
            {
                InventoryManager.Instance.PickAllFlaskToSlot(item);
            } 
            else if(item.itemData.itemType==ItemType.Equipment)
                InventoryManager.Instance.EquipItem(item.itemData);
           
        }

        public void Clear()
        {
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemText.text = "";
            item= null;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if(item == null || item.itemData == null) return;
            ui.itemTooltip.ShowToolTip(item.itemData,ui.characterPanelToolTipPosition);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if(item == null || item.itemData == null) return;
            ui.itemTooltip.HideToolTip();
        }
    }
}