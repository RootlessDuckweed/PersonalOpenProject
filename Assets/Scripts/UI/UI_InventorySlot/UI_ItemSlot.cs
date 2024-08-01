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
    public class UI_ItemSlot : MonoBehaviour,IPointerDownHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemText;

        public InventoryItem item;
        
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
            item = null;
        }
    }
}