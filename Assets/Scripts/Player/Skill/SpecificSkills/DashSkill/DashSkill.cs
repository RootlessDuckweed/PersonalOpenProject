using UI.SkillTreeUI;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace Player.Skill.SpecificSkills
{
    public class DashSkill : Skill
    {

        [Header("Dash")] public bool dashUnlocked;
        [SerializeField] private UI_SkillTreeSlot dashUnlockButton;
        public float dashShowDuration = 0.05f;
        [Header("Clone On dash")] public bool cloneOnDashUnlocked;
        [SerializeField] private UI_SkillTreeSlot cloneOnDashUnlockButton;

        [Header("Clone On arrival")] public bool cloneOnArrivalUnlocked;
        [SerializeField] private UI_SkillTreeSlot cloneOnArrivalUnlockButton;
    
        protected override void Start()
        {
            base.Start();
            dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
            cloneOnDashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
            cloneOnArrivalUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDashArrival);
            
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

        private void UnlockDash()
        {
            if(dashUnlockButton.unlocked)
                dashUnlocked = true;
            else
            {
                dashUnlocked = false;
            }
        }

        private void UnlockCloneOnDash()
        {
            if(cloneOnDashUnlockButton.unlocked)
                cloneOnDashUnlocked = true;
            else
            {
                cloneOnDashUnlocked = false;
            }
        }

        private void UnlockCloneOnDashArrival()
        {
            if(cloneOnArrivalUnlockButton.unlocked)
                cloneOnArrivalUnlocked = true;
            else
            {
                cloneOnArrivalUnlocked = false;
            }
        }
    
    }
}