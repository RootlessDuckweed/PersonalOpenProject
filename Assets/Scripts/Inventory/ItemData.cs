using UnityEngine;
using Utility.EnumType;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Item Data",menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemID;
        public ItemType itemType;
        public string itemName;
        [TextArea] public string itemDescription = "ItemDescription";
        public float itemCooldown;
        public Sprite icon;
        
        [Range(0, 100)] 
        public int dropChance;

        private void OnValidate()
        {
            #if UNITY_EDITOR
            
            string path = AssetDatabase.GetAssetPath(this);
            itemID = AssetDatabase.AssetPathToGUID(path);

            #endif
        }
    }
}