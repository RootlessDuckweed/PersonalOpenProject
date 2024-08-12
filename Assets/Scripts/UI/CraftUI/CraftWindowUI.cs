using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using TMPro;
using UI.UI_InventorySlot;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class CraftWindowUI : MonoBehaviour
    {
        public UI_EquipmentSlot selectCraftSlot;//选择制作的武器槽位
        public TextMeshProUGUI description;
        public UI_RequirementSlot[] requirementSlots; //所要材料的槽位
        public UI_ItemSlot[] selectToCraftSlots;// 从材料库中选择去制作武器的物品槽位
        public Button confirm;
        private List<InventoryItem> requiredMaterials = new List<InventoryItem>(); //所需的材料列表
        private Dictionary<ItemData, InventoryItem> selectToCraftSlotsDict = new Dictionary<ItemData, InventoryItem>(); // 从材料库中选择去制作武器的物品的字典
        private void Awake()
        {
            confirm.onClick.AddListener(Craft);
        }
        
        /// <summary>
        /// 开始制作
         /// </summary>
        private void Craft()
        {
            if (InventoryManager.Instance.CanCraft(selectCraftSlot.item.itemData as ItemDataEquipment,
                    requiredMaterials, selectToCraftSlotsDict))
            {
                ClearSelectToCraftSlots();
            }
            else
            {
                requiredMaterials.Clear();
            }
            
            
            
        }
        
        /// <summary>
        /// 添加选择的材料到制作台列表
        /// </summary>
        /// <param name="item">添加的物品</param>
        public void AddToSelectingSlots(InventoryItem item)
        {
            if(item==null||item.itemData==null)return;
            if(selectToCraftSlotsDict.ContainsKey(item.itemData)) return;
            for (int i = 0; i < selectToCraftSlots.Length; i++)
            {
                if (selectToCraftSlots[i].item==null||selectToCraftSlots[i].item.itemData == null)
                {
                    selectToCraftSlots[i].UpdateInventoryItem(item);
                    selectToCraftSlotsDict.TryAdd(item.itemData, item);
                    return;
                }
            }
        }
        
        /// <summary>
        /// 显示选择制作的物品以及所需材料
        /// </summary>
        /// <param name="item"></param>
        public void ShowTheCraftItem(InventoryItem item)
        {
            if(item==null||item.itemData==null) return;
            selectCraftSlot.UpdateInventoryItem(item);
            description.text = item.itemData.itemDescription;
            if (selectCraftSlot)
            {
                var target = selectCraftSlot.item.itemData as ItemDataEquipment;
                if (target)
                {
                    for (var i = 0; i < target.craftingMaterials.Count; i++)
                    {
                        requiredMaterials.Add( target.craftingMaterials[i]);
                        if(i<=requirementSlots.Length)
                            requirementSlots[i].UpdateInventoryItem(target.craftingMaterials[i]);
                    }
                }
            }
        }

        public void ClearSelectToCraftSlots()
        {
            for (int i = 0; i < requirementSlots.Length; i++)
            {
                requirementSlots[i].Clear();
            }
            requiredMaterials.Clear();
            
            for (int i = 0; i < selectToCraftSlots.Length; i++)
            {
                selectToCraftSlots[i].Clear();
            }
            selectToCraftSlotsDict.Clear();
            selectCraftSlot.Clear();
        }
    }
}