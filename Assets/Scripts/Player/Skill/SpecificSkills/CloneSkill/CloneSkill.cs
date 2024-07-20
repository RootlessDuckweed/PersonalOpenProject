using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Utility;

namespace Player.Skill.SpecificSkills
{
    public class CloneSkill : Skill
    {
        [SerializeField] private GameObject clonePrefab;
        [HideInInspector] private ObjectPool<GameObject> clonePool;
        [SerializeField] private float cloneDuration;
        [SerializeField] private float colorLoosingSpeed;
        [SerializeField] private bool createCloneOnDashStart;
        [SerializeField] private bool createCloneOnDashOver;
        [SerializeField] private bool createCloneOnCounterAttack;
        [SerializeField] public bool canGenerateCloneByAttack;
        private WaitForSeconds seconds = new WaitForSeconds(0.4f);
        private void Awake()
        {
            clonePool = new ObjectPool<GameObject>(CreateClone,ActionOnGet,ActionOnRelease,ActionOnDestroy,false,5);
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
            var clone =clonePool.Get();
            var cloneController = clone.GetComponent<CloneSkillController>();
            cloneController.SetupClone(PlayerManager.Instance.Player,cloneDuration,colorLoosingSpeed,Vector3.zero);
        }

        public void UseSkillAndSetPosition(Entity target,Vector3 offset)
        {
            var clone = clonePool.Get();
            var cloneController = clone.GetComponent<CloneSkillController>();
            cloneController.SetupClone(target,cloneDuration,colorLoosingSpeed,offset);
        }

        public void CreateCloneOnDashStart()
        {
            if(createCloneOnDashStart)
                UseSkill();
        }

        public void CreateCloneOnDashOver()
        {
            if(createCloneOnDashOver)
                UseSkill();
        }

        public void CreateCloneOnCounterAttack(Entity target,Vector3 offset)
        {
            if (createCloneOnCounterAttack)
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