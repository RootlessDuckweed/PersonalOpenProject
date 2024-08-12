using UI.SkillTreeUI;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Skill.SpecificSkills
{
    public class ParrySkill : Skill
    {
        [Header("Parry")] 
        [SerializeField] private UI_SkillTreeSlot parryUnlockButton;
        public bool parryUnlocked { get; private set; }

        [Header("Parry restore")] 
        [Range(0f, 1f)] public float restorePercentage;
        [SerializeField] private UI_SkillTreeSlot parryRestoreUnlockButton;
        public bool parryRestoreUnlocked { get; private set; }

        [Header("Parry with mirage")] 
        [SerializeField] private UI_SkillTreeSlot parryWithMirageUnlockButton;
        public bool parryWithMirageUnlocked{ get; private set; }
        
        protected override void Start()
        {
            base.Start();
            parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
            parryRestoreUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
            parryWithMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);
        }

        protected override void Update()
        {
            base.Update();
        }

        public override bool CanUseSkill()
        {
            if(parryUnlocked)
                return base.CanUseSkill();
            
            return false;
            
        }

        public override void UseSkill()
        {
            base.UseSkill();
        }

        private void UnlockParry()
        {
            if (parryUnlockButton.unlocked)
                parryUnlocked = true;
            else
            {
                parryUnlocked = false;
            }
        }

        private void UnlockParryRestore()
        {
            if (parryRestoreUnlockButton.unlocked)
                parryRestoreUnlocked = true;
            else
            {
                parryRestoreUnlocked = false;
            }
        }

        private void UnlockParryWithMirage()
        {
            if (parryRestoreUnlockButton.unlocked)
                parryWithMirageUnlocked = true;
            else
            {
                parryWithMirageUnlocked = false;
            }
        }
    }
}