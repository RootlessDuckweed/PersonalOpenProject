using System;
using Inventory;
using Player.Skill;
using UnityEngine;
using Utility;
using Utility.EnumType;
using Random = UnityEngine.Random;

namespace Player.Universal
{
    public class PlayerAttack : Attack_Base
    {
        private Vector3 cloneOffset;
        private void OnEnable()
        {
            cloneOffset.x = PlayerManager.Instance.Player.facingDir * 1.5f;
        }
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (stat == null)
            {
                stat = PlayerManager.Instance.Player.stats;
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                InventoryManager.Instance.GetEquipment(EquipmentType.Weapon)?.ExecuteItemSpecialEffect(PlayerManager.Instance.Player.gameObject,other.gameObject);
                
                if (SkillManager.Instance.cloneSkill.cloneSkillUnlocked)
                {
                    var chance = Random.Range(0, 100);
                    if (chance <= 20)
                    {
                        SkillManager.Instance.cloneSkill.UseSkillAndSetPosition(other.GetComponent<Enemy.Enemy>(),cloneOffset);
                    }
                }
            }
                
            base.OnTriggerEnter2D(other);

            if (isCritical)
            {
                PlayerManager.Instance.Player.fx.ScreenShake();
            }
        }
        
    }
}