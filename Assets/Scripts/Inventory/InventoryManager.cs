using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UI.UI_InventorySlot;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;
using Utility.EnumType;

namespace Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        public List<InventoryItem> startingEquipments;
        
        public List<InventoryItem> equipments;
        public Dictionary<ItemDataEquipment, InventoryItem> equipmentDictionary;

        [Header("Inventory used by material")] public List<InventoryItem> inventory;
        public Dictionary<ItemData, InventoryItem> inventoryDictionary;

        [Header("Stash used by equipment")] public List<InventoryItem> stash;
        public Dictionary<ItemData, InventoryItem> stashDictionary;

        [Header("Inventory UI")] 
        [SerializeField] private Transform inventorySlotParent;
        [SerializeField] private Transform stashSlotParent;
        [SerializeField] private Transform equipmentSlotParent;
        [SerializeField] private Transform flaskSlotParent;
        [SerializeField] private Transform statUIParent;
        private StatUI statUI;
        
        private UI_ItemSlot[] inventoryItemSlots;
        private UI_ItemSlot[] stashItemSlots;
        private UI_EquipmentSlot[] equipmentSlots;
        private UI_FlaskSlot flaskSlots;

        private float lastTimeUseFlask;
        
        protected override void Awake()
        {
            base.Awake();
            equipments = new List<InventoryItem>();
            equipmentDictionary = new Dictionary<ItemDataEquipment, InventoryItem>();

            inventory = new List<InventoryItem>();
            inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

            stash = new List<InventoryItem>();
            stashDictionary = new Dictionary<ItemData, InventoryItem>();

            statUI = statUIParent.GetComponent<StatUI>();
        }

        private void Start()
        {
            inventoryItemSlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
            stashItemSlots = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
            equipmentSlots = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
            flaskSlots = flaskSlotParent.GetComponentInChildren<UI_FlaskSlot>();
                
            foreach (var t in startingEquipments)
            {
                for (int i = 0; i < t.stackSize; i++)
                {
                    AddItem(t.itemData);
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                UseFlask();
            }
            
        }
        

        public void EquipItem(ItemData item)
        {
            if(item == null) return;
            ItemDataEquipment newEquipment = item as ItemDataEquipment;
            InventoryItem newItem = new InventoryItem(item);
            ItemDataEquipment oldEquipment =
                (from equipment in equipmentDictionary
                    where equipment.Key.equipmentType == newEquipment.equipmentType
                    select equipment.Key).FirstOrDefault();
            
            if (oldEquipment != null)
            {
                UnequipItem(oldEquipment,true);

            }

            equipments.Add(newItem);
            equipmentDictionary.Add(newEquipment, newItem);
            newEquipment.ExecuteItemNormalEffect();
            newEquipment.AddModifiers();
            RemoveItem(item);

        }

        /// <summary>
        ///  卸下装备
        /// </summary>
        /// <param name="itemToRemove"></param>
        /// <param name="comeBackInventory">是否回到背包</param>
        public void UnequipItem(ItemDataEquipment itemToRemove,bool comeBackInventory)
        {
            
            if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
            {
                equipments.Remove(value);
                equipmentDictionary.Remove(itemToRemove);
                itemToRemove.RemoveModifiers();
                itemToRemove.CancelItemNormalEffect();
                foreach (var t in equipmentSlots)
                {
                    if (itemToRemove.equipmentType == t.equipmentSlotType)
                    {
                        t.Clear();
                    }
                }
            }
            if(comeBackInventory)
                AddItem(itemToRemove);
        }


        private void UpdateSlotUIExceptFlaskUI()
        {

            foreach (var t in inventoryItemSlots)
            {
                t.Clear();
            }

            foreach (var t in stashItemSlots)
            {
                t.Clear();
            }
            
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                foreach (var t in equipmentDictionary)
                {
                    if (t.Key.equipmentType == equipmentSlots[i].equipmentSlotType)
                    {
                        equipmentSlots[i].UpdateInventoryItem(t.Value);
                    }
                }
            }

            for (int i = 0; i < inventory.Count; i++)
            {
                inventoryItemSlots[i].UpdateInventoryItem(inventory[i]);
            }

            for (int i = 0; i < stash.Count; i++)
            {
                stashItemSlots[i].UpdateInventoryItem(stash[i]);
            }
            
            statUI.UpdateValueUI();

        }

        public void AddItem(ItemData itemData)
        {
            if (itemData == null) return;
            if (itemData.itemType == ItemType.Equipment) AddToInventory(itemData);

            else if (itemData.itemType == ItemType.Material) AddToStash(itemData);

            UpdateSlotUIExceptFlaskUI();
        }

        private void AddToStash(ItemData itemData)
        {
            if (stashDictionary.TryGetValue(itemData, out InventoryItem value))
            {
                value.AddStack();
            }
            else
            {
                var newItem = new InventoryItem(itemData);
                stash.Add(newItem);
                stashDictionary.Add(itemData, newItem);
            }
        }

        private void AddToInventory(ItemData itemData)
        {
            
            if (inventoryDictionary.TryGetValue(itemData, out InventoryItem value))
            {
                value.AddStack();
            }
            else
            {
                var newItem = new InventoryItem(itemData);
                inventory.Add(newItem);
                inventoryDictionary.Add(itemData, newItem);
            }
        }


        public void RemoveItem(ItemData itemData)
        {
            if(itemData == null) return;
            if (inventoryDictionary.TryGetValue(itemData, out InventoryItem value))
            {
                if (value.stackSize <= 1)
                {
                    inventory.Remove(value);
                    inventoryDictionary.Remove(itemData);

                }
                else
                {
                    value.RemoveStack();
                }
            }

            if (stashDictionary.TryGetValue(itemData, out InventoryItem stash))
            {
                if (stash.stackSize <= 1)
                {
                    this.stash.Remove(stash);
                    stashDictionary.Remove(itemData);
                }
                else
                {
                    stash.RemoveStack();
                }
            }
            UpdateSlotUIExceptFlaskUI();
        }
        
        
        public bool CanCraft(ItemDataEquipment itemToCraft,List<InventoryItem> reqInventoryItems)
        {
            List<InventoryItem> materialToRemove = new ();
            foreach (var t in reqInventoryItems)
            {
                if(stashDictionary.TryGetValue(t.itemData,out InventoryItem value))
                {
                    //add
                    if (value.stackSize < t.stackSize)
                    {
                        return false;
                    }
                    materialToRemove.Add(value);
                }
                else
                {
                    return false;
                }
            }

            foreach (var t in materialToRemove)
            {
                RemoveItem(t.itemData);
            }
            AddItem(itemToCraft);
            return true;
        }
        
        public ItemDataEquipment GetEquipment(EquipmentType type)
        {
            foreach (var t in equipmentDictionary)
            {
                if (t.Key.equipmentType == type)
                {
                    var equippedItem = t.Key;
                    return equippedItem;
                }
            }

            return null;
        }


        #region Flask
        
        public void UseFlask()
        {
            if( flaskSlots.item==null) return;
            
            var flask = flaskSlots.item.itemData as ItemDataEquipment;
            
            if(flask == null) return; 
            
            bool canUseFlask = Time.time - lastTimeUseFlask >= flask.itemCooldown;
            if (canUseFlask)
            {
                flask.ExecuteItemSpecialEffect();
                lastTimeUseFlask = Time.time; 
                flaskSlots.item.RemoveStack();
                flaskSlots.UpdateInventoryItem(flaskSlots.item);
            }
        }
        public void PickAllFlaskToSlot(InventoryItem item)
        {
            if(item == null) return;
            if (inventoryDictionary.TryGetValue(item.itemData, out InventoryItem value))
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(value.itemData);
                if (flaskSlots.item!=null&&flaskSlots.item.itemData!=null)
                {
                    inventory.Add(flaskSlots.item);
                    inventoryDictionary.Add(flaskSlots.item.itemData,flaskSlots.item);
                }
                flaskSlots.Clear();
                flaskSlots.item = value;
                flaskSlots.UpdateInventoryItem(flaskSlots.item);
            }
            UpdateSlotUIExceptFlaskUI();
        }
        
         #endregion
        
    }
    
}