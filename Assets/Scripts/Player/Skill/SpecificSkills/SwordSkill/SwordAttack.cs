using Player.Universal;
using UnityEngine;
using Utility;
using Utility.EnumType;

namespace Player.Skill.SpecificSkills.SwordSkill
{
    public class SwordAttack : Attack_Base
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (stat == null)
            {
                stat = PlayerManager.Instance.Player.stats;
                damageType = DamageType.Physical;
            }

            float defaultWeaponDamage = originalWeaponDamage;
            if (SkillManager.Instance.swordSkill.vulnerableUnlocked)
                originalWeaponDamage += originalWeaponDamage * 0.1f;
            Enemy.Enemy enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null && !enemy.stats.IsEvasion())
            {
                if(SkillManager.Instance.swordSkill.timeStopUnlocked)
                    enemy.StartCoroutine("FreezeTimeFor", SkillManager.Instance.swordSkill.freezeTimeDuration);
            }
            base.OnTriggerEnter2D(other);
            originalWeaponDamage = defaultWeaponDamage;
        }
        
    }
}