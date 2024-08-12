using Inventory;
using Player.Universal;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;
using Utility.EnumType;
using Random = UnityEngine.Random;

namespace Player.Skill.SpecificSkills.CloneSkill
{
    public class CloneAttack : Attack_Base
    {
        
       
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (stat == null)
            {
                stat = PlayerManager.Instance.Player.stats;
            }
            if (SkillManager.Instance.cloneSkill.multiCloneUnlocked)
            {
                if (Random.value <= 0.2f)
                {
                    Enemy.Enemy enemy = other.GetComponent<Enemy.Enemy>();
                    if (enemy != null && !enemy.stats.IsEvasion())
                    {
                        SkillManager.Instance.cloneSkill.UseSkillAndSetPosition(enemy,new Vector2(transform.parent.parent.localScale.x*1.5f,0));
                    }
                    else
                    {
                        return;
                    }
                }
            }

            float defaultDamage = originalWeaponDamage;
            
            if (SkillManager.Instance.cloneSkill.aggressiveCloneUnlocked)
            {
                originalWeaponDamage += originalWeaponDamage*0.8f;
            }

            if (SkillManager.Instance.cloneSkill.cloneInheritWeaponEffectUnlocked)
            {
                InventoryManager.Instance.GetEquipment(EquipmentType.Weapon)?.ExecuteItemSpecialEffect(PlayerManager.Instance.Player.gameObject,other.gameObject);
            }
            
            base.OnTriggerEnter2D(other);

            originalWeaponDamage = defaultDamage;
        }
    }
}