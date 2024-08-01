using UnityEngine;

namespace Player.Skill.SpecificSkills.BlackHoleSkill
{
    public class BlackHoleSkill : Skill
    {
        [SerializeField] private GameObject blackHolePrefab;
        [SerializeField] private float maxSize;
        [SerializeField] private float growSpeed;
        [SerializeField] private float shrinkSpeed;
        [SerializeField] private int amountOfAttacks;
        [SerializeField] private float cloneAttackCooldown;
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            //print(coolDownTimer);
        }

        public override bool CanUseSkill()
        {
            return base.CanUseSkill();
        }

        public override void UseSkill()
        {
            base.UseSkill();
            GameObject blackHole = Instantiate(blackHolePrefab,player.transform.position,Quaternion.identity);
            
            blackHole.GetComponent<BlackHoleSkillController>().SetupBlackHole(maxSize,growSpeed,shrinkSpeed,amountOfAttacks,cloneAttackCooldown);
           
        }
    }
}