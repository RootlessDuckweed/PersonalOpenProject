using Character;
using UI.SkillTreeUI;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Skill.SpecificSkills.DodgeSkill
{
    public class DodgeSkill : Skill
    {
        [Header("Dodge")] 
        [SerializeField] private UI_SkillTreeSlot unlockDodgeButton;
        public bool dodgeUnlocked;

        [Header("Mirage dodge")] 
        [SerializeField] private UI_SkillTreeSlot unlockMirageDodgeButton;
        public bool dodgeMirageUnlocked;
        protected override void Start()
        {
            base.Start();
            unlockDodgeButton.GetComponent<Button>().onClick.AddListener(UnlockDodge);
            unlockMirageDodgeButton.GetComponent<Button>().onClick.AddListener(UnlockMirageDodge);
        }

        protected override void Update()
        {
            base.Update();
        }

        public override bool CanUseSkill()
        {
            return base.CanUseSkill();
        }

        public override void UseSkill()
        {
            base.UseSkill();
        }

        private void UnlockDodge()
        {
            if(dodgeUnlocked) return;
            if (unlockDodgeButton.unlocked)
            {
                dodgeUnlocked = true;
                player.stats.evasion.AddModifier(10);
                var stats = player.stats as PlayerStats;
                stats?.OnStatsChanged.Invoke();
            }
            else
            {
                dodgeUnlocked = false;
                player.stats.evasion.RemoveModifier(10);
                var stats = player.stats as PlayerStats;
                stats?.OnStatsChanged.Invoke();
            }
        }

        private void UnlockMirageDodge()
        {
            if (unlockMirageDodgeButton.unlocked)
                dodgeMirageUnlocked = true;
            else
            {
                dodgeMirageUnlocked = false;
            }
        }

        public void CreateCloneOnDodge()
        {
            print(player==null);
            print("dodge mirage"+dodgeMirageUnlocked);
            if (dodgeMirageUnlocked)
            {
                SkillManager.Instance.cloneSkill.UseSkillAndSetPosition(player,Vector3.zero);
            }
        }
    }
}