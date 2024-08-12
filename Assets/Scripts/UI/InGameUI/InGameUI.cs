using System;
using Inventory;
using Player.Skill;
using Player.Universal;
using TMPro;
using UI.UI_InventorySlot;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGameUI
{
    public class InGameUI : MonoBehaviour
    {
        [Header("Skill UI")]
        [SerializeField] private Image blackHoleCooldownImage;
        [SerializeField] private Image dashCooldownImage;
        [SerializeField] private Image crystalCoolDownImage;
        [SerializeField] private Image parryCoolDownImage;
        [SerializeField] private Image swordCoolDownImage;

        [Header("Flask UI")] 
        [SerializeField] private Image flaskCoolDownImage;
        [SerializeField] private Image flaskUIImage;
        [SerializeField] private UI_FlaskSlot flaskSlot;
        [SerializeField] private TextMeshProUGUI amountText;
        private Sprite defaultImage;

        private void Awake()
        {
            defaultImage = flaskUIImage.sprite;
            
        }

        private void Start()
        {
            InventoryManager.Instance.OnFlaskUsed += SetFlaskFillAmount;
        }

        private void FixedUpdate()
        {
            BlackHoleCoolDownCounter();
            DashCoolDownCounter();
            CrystalCoolDownCounter();
            ParryCoolDownCounter();
            SwordCoolDownCounter();
            FlaskCoolDownCounter(flaskSlot);
        }

        private void BlackHoleCoolDownCounter()
        {
            if (!SkillManager.Instance.cloneSkill.blackHoleSkillUnlocked)
            {
                  blackHoleCooldownImage.fillAmount = 1;
                  return;
            }
            blackHoleCooldownImage.fillAmount = SkillManager.Instance.blackHoleSkill.coolDownTimer /
                                                SkillManager.Instance.blackHoleSkill.coolDown;
        }

        private void DashCoolDownCounter()
        {
            if (!SkillManager.Instance.dashSkill.dashUnlocked)
            {
                 dashCooldownImage.fillAmount = 1;
                 return;
            }

            dashCooldownImage.fillAmount = PlayerManager.Instance.Player.dashColdTimeCounter /
                                           PlayerManager.Instance.Player.dashColdTime;
        }

        private void CrystalCoolDownCounter()
        {
            if (!SkillManager.Instance.crystalSkill.crystalUnlock)
            {
                crystalCoolDownImage.fillAmount = 1;
                return;
            }

            if (!SkillManager.Instance.crystalSkill.crystalMultiControlledUnlock)
            {
                crystalCoolDownImage.fillAmount = SkillManager.Instance.crystalSkill.coolDownTimer /
                                                  SkillManager.Instance.crystalSkill.coolDown;
            }
            else
            {
                crystalCoolDownImage.fillAmount = SkillManager.Instance.crystalSkill.multiStackTimer /
                                                  SkillManager.Instance.crystalSkill.multiStackCooldown;
            }
        }

        private void ParryCoolDownCounter()
        {
            if (!SkillManager.Instance.parrySkill.parryUnlocked)
            {
                parryCoolDownImage.fillAmount = 1;
                return;
            }

            parryCoolDownImage.fillAmount = SkillManager.Instance.parrySkill.coolDownTimer /
                                            SkillManager.Instance.parrySkill.coolDown;
        }

        private void SwordCoolDownCounter()
        {
            if (!SkillManager.Instance.swordSkill.swordUnlocked)
            {
                swordCoolDownImage.fillAmount = 1;
                return;
            }

            swordCoolDownImage.fillAmount = SkillManager.Instance.swordSkill.coolDownTimer /
                                            SkillManager.Instance.swordSkill.coolDown;
        }

        private void FlaskCoolDownCounter(UI_FlaskSlot flask)
        {
            amountText.text = flaskSlot.itemText.text;
            if (flask.item == null || flask.item.itemData == null)
            {
                flaskUIImage.sprite = defaultImage;
                flaskCoolDownImage.sprite = defaultImage;
                flaskCoolDownImage.fillAmount = 0;
                return;
            }
            flaskUIImage.sprite = flask.item.itemData.icon;
            flaskCoolDownImage.sprite = flaskUIImage.sprite;
            if( flaskCoolDownImage.fillAmount>0f)
                flaskCoolDownImage.fillAmount -= 1/ flask.item.itemData.itemCooldown*Time.deltaTime;
        }

        private void SetFlaskFillAmount()
        {
            flaskCoolDownImage.fillAmount = 1;
        }
    }
}