using Player.Skill.SpecificSkills.CrystalSkill;
using Player.Skill.SpecificSkills;
using Player.Skill.SpecificSkills.BlackHoleSkill;
using Player.Skill.SpecificSkills.CloneSkill;
using Player.Skill.SpecificSkills.DodgeSkill;
using Player.Skill.SpecificSkills.SwordSkill;
using Utility;

namespace Player.Skill
{
    public class SkillManager : Singleton<SkillManager>
    {
        public DashSkill dashSkill { get; private set; }
        public CloneSkill cloneSkill { get; private set; }
        public SwordSkill swordSkill { get; private set; }
        public BlackHoleSkill blackHoleSkill { get; private set; }
        public CrystalSkill crystalSkill { get; private set; }
        public ParrySkill parrySkill { get; private set; }
        public DodgeSkill dodgeSkill {get; private set;  }

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            dashSkill = GetComponent<DashSkill>();
            cloneSkill = GetComponent<CloneSkill>();
            swordSkill = GetComponent<SwordSkill>();
            blackHoleSkill = GetComponent<BlackHoleSkill>();
            cloneSkill = GetComponent<CloneSkill>();
            crystalSkill = GetComponent<CrystalSkill>();
            parrySkill = GetComponent<ParrySkill>();
            dodgeSkill = GetComponent<DodgeSkill>();
        }
    }
}