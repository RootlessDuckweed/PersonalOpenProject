using System;
using Player.Universal;
using SaveAndLoad;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.SkillTreeUI
{
    public class UI_SkillTreeSlot : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler,ISaveManager,IPointerDownHandler
    {
        private UI ui;
        [SerializeField] private string skillName;
        [TextArea] [SerializeField] private string skillDescription;
        public bool unlocked;
        [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
        [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;
        [FormerlySerializedAs("nextSkillSlot")] [SerializeField] private UI_SkillTreeSlot[] nextSkillSlots;
        [SerializeField] private Image skillImage;

        [SerializeField] private int needPrice;
        private void OnValidate()
        {
            gameObject.name = $"SkillTreeSlot_UI-{skillName}";
        }

        private void Awake()
        {
            ui = GetComponentInParent<UI>();
            skillImage = GetComponent<Image>();
            skillImage.color = Color.gray;
            GetComponent<Button>().onClick.AddListener(UnlockSkillSlot);
        }
        

        public void UnlockSkillSlot()
        {
            if(Keyboard.current.leftCtrlKey.isPressed) return;
            if (PlayerManager.Instance.HaveEnoughMoney(needPrice) == false)
                return;
            foreach (var t in shouldBeUnlocked)
            {
                if(t.unlocked == false) return;
            }

            foreach (var t in shouldBeLocked)
            {
                if(t.unlocked) return;
            }


            unlocked = true;
            skillImage.color = Color.white;
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ui.skillToolTip.ShowToolTip(skillDescription,skillName);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ui.skillToolTip.HideToolTip();
        }

        public void LoadData(GameData data)
        {
            if (data.skillTree.TryGetValue(skillName, out unlocked))
            {
                unlocked = data.skillTree[skillName];
            }
            else
            {
                unlocked = false;
            }
            if (unlocked)
            {
                skillImage.color = Color.white;
                GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                skillImage.color = Color.gray;
            }
        }

        public void SaveData(ref GameData data)
        {
            data.skillTree[skillName] = unlocked;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if(!unlocked) return;
            if (Keyboard.current.leftCtrlKey.isPressed)
            {
                foreach (var t in nextSkillSlots)
                {
                    if (t.unlocked)
                    {
                       return;
                    }
                    
                }
                unlocked = false;
                GetComponent<Button>().onClick.Invoke();
                skillImage.color = Color.gray;
            }
        }
    }
}