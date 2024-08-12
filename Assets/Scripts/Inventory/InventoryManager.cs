using System.Collections.Generic;
using System.Linq;
using SaveAndLoad;
using UI;
using UI.UI_InventorySlot;
using UnityEngine;
using UnityEngine.Events;
using Utility;
using Utility.EnumType;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Inventory
{
    public class InventoryManager : Singleton<InventoryManager>,ISaveManager
    {
        public List<InventoryItem> startingEquipments;
        
        public List<InventoryItem> equipments;
        public Dictionary<ItemDataEquipment, InventoryItem> equipmentDictionary;

        [Header("Inventory used by equipment")] public List<InventoryItem> inventory;
        public Dictionary<ItemData, InventoryItem> inventoryDictionary;

        [Header("Stash used by material")] public List<InventoryItem> stash;
        public Dictionary<ItemData, InventoryItem> stashDictionary;

        [Header("Inventory UI")] 
        [SerializeField] private Transform inventorySlotParent;
        [SerializeField] private Transform stashSlotParent;
        [SerializeField] private Transform equipmentSlotParent;
        [SerializeField] private Transform flaskSlotParent;
        
        [Header("Player Stats UI")]
        [SerializeField] private Transform statUIParent;
        private StatUI statUI;
        
        private UI_ItemSlot[] inventoryItemSlots;
        private UI_StashSlot[] stashItemSlots;
        private UI_EquipmentSlot[] equipmentSlots;
        private UI_FlaskSlot flaskSlots; //血瓶 直接绑带槽位的item，不在这里做缓存 

        public float lastTimeUseFlask { get; private set; }
        public UnityAction OnFlaskUsed;

        [Header("Data base")] 
        private string[] assetNames;
        public List<ItemData> itemDataBase;
        public List<InventoryItem> loadedItems;
        public List<ItemDataEquipment> loadedEquipments;
        protected override void Awake()
        {
            base.Awake();
            equipments = new List<InventoryItem>();
            equipmentDictionary = new Dictionary<ItemDataEquipment, InventoryItem>();

            inventory = new List<InventoryItem>();
            inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

            stash = new List<InventoryItem>();
            stashDictionary = new Dictionary<ItemData, InventoryItem>();
            
            inventoryItemSlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>(true);
            stashItemSlots = stashSlotParent.GetComponentsInChildren<UI_StashSlot>(true);
            equipmentSlots = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>(true);
            flaskSlots = flaskSlotParent.GetComponentInChildren<UI_FlaskSlot>(true);
            statUI = statUIParent.GetComponent<StatUI>();
        }
        

        private void Start()
        {
            
            
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
        
        public void AddItem(ItemData itemData,int size)
        {
            if (itemData == null) return;
            if (itemData.itemType == ItemType.Equipment)
            {
                if (flaskSlots.item!=null && itemData == flaskSlots.item.itemData)
                {
                    flaskSlots.item.stackSize += size;
                    flaskSlots.UpdateInventoryItem(flaskSlots.item);
                    return;
                }
            
                if (inventoryDictionary.TryGetValue(itemData, out InventoryItem value))
                {
                    value.stackSize += size;
                }
                else
                {
                    var newItem = new InventoryItem(itemData,size);
                    inventory.Add(newItem);
                    inventoryDictionary.Add(itemData, newItem);
                }
            }

            else if (itemData.itemType == ItemType.Material)
            {
                if (stashDictionary.TryGetValue(itemData, out InventoryItem value))
                {
                    value.stackSize += size;
                }
                else
                {
                    var newItem = new InventoryItem(itemData,size);
                    stash.Add(newItem);
                    stashDictionary.Add(itemData, newItem);
                }
            }

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
            if(flaskSlots==null)return;
            if (flaskSlots.item!=null && itemData == flaskSlots.item.itemData)
            {
                flaskSlots.item.AddStack();
                flaskSlots.UpdateInventoryItem(flaskSlots.item);
                return;
            }
            
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
/// <summary>
/// 制作装备时 移除材料的移除函数
/// </summary>
/// <param name="NeedToCraftItem">拿去制造的材料</param>
        public void RemoveMaterialToCraft(InventoryItem NeedToCraftItem)
        {
            if (stashDictionary.TryGetValue(NeedToCraftItem.itemData, out InventoryItem stash))
            {
                stash.stackSize -= NeedToCraftItem.stackSize;
                if (stash.stackSize < 1)
                {
                    this.stash.Remove(stash);
                    stashDictionary.Remove(NeedToCraftItem.itemData);
                }
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
        
        
        public bool CanCraft(ItemDataEquipment itemToCraft,List<InventoryItem> reqInventoryItems,Dictionary<ItemData,InventoryItem> selectToCraftSlotsDict)
        {
            
            List<InventoryItem> materialToRemove = new ();
            foreach (var t in reqInventoryItems)
            {
                if(selectToCraftSlotsDict.TryGetValue(t.itemData,out InventoryItem value))
                {
                    //add
                    if (value.stackSize < t.stackSize)
                    {
                        return false;
                    }
                    
                    materialToRemove.Add(new InventoryItem(t.itemData,t.stackSize));
                }
                else
                {
                    return false;
                }
            }

            foreach (var t in materialToRemove)
            {
                RemoveMaterialToCraft(t); // 移除需要的材料
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
                OnFlaskUsed?.Invoke();
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

         public void LoadData(GameData data)
         {
             LoadInventoryAndStash(data);
                
             LoadEquipments(data);

             LoadFlaskSlot(data);
         }

         public void NewGameAddOriginalItem()
         {
             if (startingEquipments.Count > 0)
             {
                 foreach (var t in startingEquipments)
                 {
                     for (int i = 0; i < t.stackSize; i++)
                     {
                         AddItem(t.itemData);
                     }
                 }
             }
         }

         private void LoadFlaskSlot(GameData data)
         {
             foreach (var item in itemDataBase)
             {
                 if (item.itemID == data.flaskSlot.Key)
                 {
                     flaskSlots.UpdateInventoryItem(new InventoryItem(item,data.flaskSlot.Value));
                 }
             }
         }

         private void LoadEquipments(GameData data)
         {
             if(data.equipmentID==null) return;
             foreach (var t in data.equipmentID)
             {
                 foreach (var item in itemDataBase)
                 {
                     if (item.itemID == t)
                     {
                         loadedEquipments.Add(item as ItemDataEquipment);
                     }
                 }
             }

             foreach (var t in loadedEquipments)
             {
                 EquipItem(t);
             }
         }

         private void LoadInventoryAndStash(GameData data)
         {
             foreach (var t in data.inventory)
             {
                 foreach (var item in itemDataBase)
                 {
                     if (item.itemID == t.Key)
                     {
                         InventoryItem itemToLoad = new(item,t.Value);
                         loadedItems.Add(itemToLoad);
                     }
                 }
             }

             foreach (var loadedItem in loadedItems)
             {
                 AddItem(loadedItem.itemData,loadedItem.stackSize);
             }
         }

         public void SaveData(ref GameData data)
         {
             data.inventory.Clear();
             data.equipmentID.Clear();
             foreach (var t in inventoryDictionary)
             {
                 data.inventory.Add(t.Key.itemID,t.Value.stackSize);
             }

             foreach (var t in stashDictionary)
             {
                 data.inventory.Add(t.Key.itemID,t.Value.stackSize);
             }

             foreach (var t in equipmentDictionary)
             {
                 data.equipmentID.Add(t.Key.itemID);
             }

             if (flaskSlots.item != null && flaskSlots.item.itemData)
             {
                 data.flaskSlot =
                     new KeyValuePair<string, int>(flaskSlots.item.itemData.itemID, flaskSlots.item.stackSize);
             }
         }
         
#if UNITY_EDITOR
         [ContextMenu("Fill up item data base")]
         private void FillUpItemDataBase() => itemDataBase = GetItemDataBase();

         private List<ItemData> GetItemDataBase()
         {
             itemDataBase = new List<ItemData>();
             // it returns a GUID array 
             assetNames = AssetDatabase.FindAssets("t:ScriptableObject", new string[]{"Assets/ItemSO"});
             foreach (var SOName in assetNames)
             {
                 var SOPath = AssetDatabase.GUIDToAssetPath(SOName);
                 var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOPath);
                 itemDataBase.Add(itemData);
             }

             return itemDataBase;
         }
#endif
        
    }
    
}