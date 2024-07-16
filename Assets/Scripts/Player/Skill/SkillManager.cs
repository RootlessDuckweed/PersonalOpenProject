using System;
using Player.Skill.SpecificSkills.CrystalSkill;
using UnityEngine;
using Player.Skill.SpecificSkills;
namespace Player.Skill
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager Instance { get; private set; }
        public DashSkill dashSkill { get; private set; }
        public CloneSkill cloneSkill { get; private set; }
        public SwordSkill swordSkill { get; private set; }
        public BlackHoleSkill blackHoleSkill { get; private set; }
        public CrystalSkill crystalSkill { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            dashSkill = GetComponent<DashSkill>();
            cloneSkill = GetComponent<CloneSkill>();
            swordSkill = GetComponent<SwordSkill>();
            blackHoleSkill = GetComponent<BlackHoleSkill>();
            cloneSkill = GetComponent<CloneSkill>();
            crystalSkill = GetComponent<CrystalSkill>();
        }
    }
}