using System;
using System.Collections.Generic;
using DG.Tweening;
using Inventory;
using SceneManager;
using UI.SkillTreeUI;
using UI.UI_InventorySlot;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class UI : MonoBehaviour
    {
        public UI_ItemTooltip itemTooltip;
        [SerializeField] private GameObject deadPanel; 
        
        [Header("Craft Panel details")]
        public RectTransform craftPanelItemToolTipPosition;
        [SerializeField] private GameObject craftSlotParent;
        public CraftWindowUI craftWindowUI;
        private  UI_CraftSlot[] craftSlots;
        public List<ItemDataEquipment> canBeCraftedEquipments;
        
        [Header("Character Panel details")]
        [SerializeField] private GameObject characterPanel;
        public RectTransform characterPanelToolTipPosition;

        [Header("Skill Tree Panel details")] 
        public UI_SkillToolTip skillToolTip;
        [SerializeField] private GameObject skillTreePanel;

        [Header("InGameUI details")] 
        [SerializeField] private GameObject inGamePanel;
        private PlayerInputSettings playerUIInput;
        private bool isOpened;
        

        private void Awake()
        {
            playerUIInput = new PlayerInputSettings();
            skillTreePanel.SetActive(true); // need to initialize
        }

        private void OnEnable()
        {
            playerUIInput.UI.Enable();
        }

        private void Start()
        {
            craftSlots = craftSlotParent.GetComponentsInChildren<UI_CraftSlot>();
            SwitchTo(null);
            
            playerUIInput.UI.OpenUI.performed += OpenUI();

            for (int i = 0; i < canBeCraftedEquipments.Count; i++)
            {
                var item = new InventoryItem(canBeCraftedEquipments[i]);
                craftSlots[i].UpdateInventoryItem(item);
            }
        }

        private Action<InputAction.CallbackContext> OpenUI()
        {
            return (obj) =>
            {
                if (!isOpened)
                {
                    SwitchTo(characterPanel);
                    inGamePanel.SetActive(false);
                }
                else
                {
                    SwitchTo(null);
                    inGamePanel.SetActive(true);
                }
                isOpened =!isOpened;
                GameManager.GameManager.Instance.PauseGame(isOpened);
            };
        }

        private void OnDisable()
        {
            playerUIInput.UI.Disable();
        }

        public void SwitchTo(GameObject menu)
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            if (menu != null)
            {
                menu.SetActive(true);
            }
        }

        public void DeadTextAppear()
        {
            deadPanel.SetActive(true);
            var buttons = deadPanel.GetComponentsInChildren<Button>(true);
            buttons[0].onClick.AddListener(SceneLoaderManager.Instance.ComeBackMainMenu);
            buttons[1].onClick.AddListener(SceneLoaderManager.Instance.ReStart);
            deadPanel.transform.DOScale(1, 1f);
        }
    }
}