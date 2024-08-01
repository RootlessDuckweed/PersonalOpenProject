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
            
            
            base.OnTriggerEnter2D(other);
        }
        
    }
}