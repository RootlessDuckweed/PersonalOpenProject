using Player.Universal;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Player.Skill.SpecificSkills.CloneSkill
{
    public class CloneAttack : Attack_Base
    {
        public bool canGenerateCloneByAttack;
        
        private void Start()
        {
            canGenerateCloneByAttack = SkillManager.Instance.cloneSkill.canGenerateCloneByAttack;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (stat == null)
            {
                stat = PlayerManager.Instance.Player.stats;
            }
            if (canGenerateCloneByAttack)
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
            
            base.OnTriggerEnter2D(other);
        }
    }
}