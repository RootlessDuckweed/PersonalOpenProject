using System.Collections;
using Player.Universal;
using UI.SkillTreeUI;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Utility;

namespace Player.Skill.SpecificSkills.CloneSkill
{
    public class CloneSkill : Skill
    {
        [SerializeField] private GameObject clonePrefab;
        private ObjectPool<GameObject> clonePool;
        [SerializeField] private float cloneDuration;
        [SerializeField] private float colorLoosingSpeed;

        [Header("Clone Skill")]
        [SerializeField] private UI_SkillTreeSlot cloneSkillUnlockButton;
        public bool cloneSkillUnlocked { get; private set; }

        [Header("BlackHole Skill")]
        [SerializeField] private UI_SkillTreeSlot blackHoleSkillUnlockButton;
        public bool blackHoleSkillUnlocked { get; private set; }

        [Header("Aggressive clone")] 
        [SerializeField] private UI_SkillTreeSlot aggressiveCloneUnlockButton;
        public bool aggressiveCloneUnlocked { get; private set; }

        [Header("Multi clone")] 
        [SerializeField] private UI_SkillTreeSlot multiCloneUnlockButton;
        public bool multiCloneUnlocked{ get; private set; }

        [Header("Clone Inherit weapon effect")] 
        [SerializeField] private UI_SkillTreeSlot cloneInheritWeaponEffectUnlockButton;
        public bool cloneInheritWeaponEffectUnlocked { get; private set; }
        
        private WaitForSeconds seconds = new WaitForSeconds(0.4f);
        private void Awake()
        {
            clonePool = new ObjectPool<GameObject>(CreateClone,ActionOnGet,ActionOnRelease,ActionOnDestroy,false,5);
        }

        protected override void Start()
        {
            base.Start();
            cloneSkillUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneSkill);
            blackHoleSkillUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBlackHoleSkill);
            aggressiveCloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockAggressiveClone);
            multiCloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMultiClone);
            cloneInheritWeaponEffectUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneInheritEffect);
        }

        protected override void Update()
        {
            base.Update();
        }

        public override bool CanUseSkill()
        {
            return base.CanUseSkill();
        }

        #region UI_Unlock

        private void UnlockCloneSkill()
        {
            if (cloneSkillUnlockButton.unlocked)
                cloneSkillUnlocked = true;
            else
            {
                cloneSkillUnlocked = false;
            }
        }

        private void UnlockBlackHoleSkill()
        {
            if (blackHoleSkillUnlockButton.unlocked)
                blackHoleSkillUnlocked = true;
            else
            {
                blackHoleSkillUnlocked = false;
            }
        }

        private void UnlockAggressiveClone()
        {
            if (aggressiveCloneUnlockButton.unlocked)
                aggressiveCloneUnlocked = true;
            else
            {
                aggressiveCloneUnlocked = false;
            }
        }

        private void UnlockMultiClone()
        {
            if (multiCloneUnlockButton.unlocked)
                multiCloneUnlocked = true;
            else
            {
                multiCloneUnlocked = false;
            }
        }

        private void UnlockCloneInheritEffect()
        {
            if (cloneInheritWeaponEffectUnlockButton.unlocked)
                cloneInheritWeaponEffectUnlocked = true;
            else
            {
                cloneInheritWeaponEffectUnlocked = false;
            }
        }

        #endregion

        public override void UseSkill()
        {
            if(!cloneSkillUnlocked) return;
            
            base.UseSkill();
            var clone =clonePool.Get();
            var cloneController = clone.GetComponent<CloneSkillController>();
            cloneController.SetupClone(PlayerManager.Instance.Player,cloneDuration,colorLoosingSpeed,Vector3.zero);
        }

        public void UseSkillAndSetPosition(Entity target,Vector3 offset)
        {
            if(!cloneSkillUnlocked || target==null)return;
            
            var clone = clonePool.Get();
            var cloneController = clone.GetComponent<CloneSkillController>();
            cloneController.SetupClone(target,cloneDuration,colorLoosingSpeed,offset);
        }
        
        public void CreateCloneOnDashStart()
        {
            if( SkillManager.Instance.dashSkill.cloneOnDashUnlocked)
                UseSkill();
        }

        public void CreateCloneOnDashOver()
        {
            if(SkillManager.Instance.dashSkill.cloneOnArrivalUnlocked)
                UseSkill();
        }

        public void CreateCloneOnCounterAttack(Entity target,Vector3 offset)
        {
            if (SkillManager.Instance.parrySkill.parryWithMirageUnlocked)
            {
                StartCoroutine(CreateCloneWithDelay(target, offset));
            }
        }

        private IEnumerator CreateCloneWithDelay(Entity target,Vector3 offset)
        {
            yield return seconds;
            UseSkillAndSetPosition(target,offset);
        }
        
        public void ReleaseCloneObj(GameObject obj)
        {
            clonePool.Release(obj);
        }
        
        private GameObject CreateClone()
        {
            var obj =  Instantiate(clonePrefab);
            obj.SetActive(false);
            return obj;
        }

        private void ActionOnGet(GameObject clone)
        {
            clone.gameObject.SetActive(true);
        }

        private void ActionOnRelease(GameObject clone)
        {
            clone.gameObject.SetActive(false);
        }

        private void ActionOnDestroy(GameObject clone)
        {
            Destroy(clone);
        }
    }
}