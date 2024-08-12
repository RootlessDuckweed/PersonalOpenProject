using Inventory;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UI_ItemTooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemTypeText;
        [SerializeField] private TextMeshProUGUI itemDescription;

        public void ShowToolTip(ItemData itemData,RectTransform rectTransform = null)
        {
            itemNameText.text = itemData.itemName;
            var equipment = itemData as ItemDataEquipment;
            itemTypeText.text = equipment != null ? equipment.equipmentType.ToString() : itemData.itemType.ToString();
            itemDescription.text = itemData.itemDescription;
            if(rectTransform!=null)
                gameObject.transform.position = rectTransform.position;
            gameObject.SetActive(true);
        }

        public void HideToolTip() => gameObject.SetActive(false);
    }
}