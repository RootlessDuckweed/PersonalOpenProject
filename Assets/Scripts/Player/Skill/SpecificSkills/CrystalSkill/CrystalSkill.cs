using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

namespace Player.Skill.SpecificSkills.CrystalSkill
{
    public class CrystalSkill : Skill
    {
        [SerializeField] private float crystalDuration;
        [SerializeField] private GameObject crystalPrefab;
        
        [Header("Move")]
        [SerializeField] private bool canMove;
        [SerializeField] private float moveSpeed;
        
        [Header("Explode")]
        [SerializeField] private bool canExplode;

        [Header("Multi stacking crystal")] 
        [SerializeField] private bool canUseMultiStacks;
        [SerializeField] private int amountOfStacks;
        [SerializeField] private float multiStackCooldown;

        [Header("Crystal mirage")] 
        [SerializeField] private bool cloneInsteadOfCrystal; 
        
        private float multiStackTimer;
        public int currentStacks { get; private set; }

        private GameObject currentCrystal;

        private ObjectPool<GameObject> crystalPool;

        private void Awake()
        {
            crystalPool = new ObjectPool<GameObject>(CreateCrystal,ActionOnGet, ActionOnRelease,ActionOnDestroy,true,3);
        }

        protected override void Start()
        {
            base.Start();
            currentStacks = amountOfStacks;
        }

        protected override void Update()
        {
            base.Update();
            multiStackTimer -= Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.F))
            {
                CanUseSkill();
            }
        }

        public override bool CanUseSkill()
        {
            return base.CanUseSkill();
        }

        public override void UseSkill()
        {
            base.UseSkill();

            if (CanUseMultiCrystal())
            {
                return;
            }
            
            CanUseRegularCrystal();
        }

        private bool CanUseRegularCrystal()
        {
            if(canUseMultiStacks) return false;

            if (currentCrystal==null)
            {
                currentCrystal = GetCrystalFromPool(player.transform,quaternion.identity);
                //currentCrystal = Instantiate(crystalPrefab, player.transform.position, quaternion.identity);
                currentCrystal.GetComponent<CrystalSkillController>().SetupCrystal(crystalDuration,canExplode,canMove,moveSpeed,FindClosestEnemy(currentCrystal.transform));
            }
            else
            {

                if (cloneInsteadOfCrystal)
                {
                    SkillManager.Instance.cloneSkill.UseSkillAndSetPosition(player,Vector3.zero);
                    (player.transform.position, currentCrystal.transform.position) = (currentCrystal.transform.position, player.transform.position);
                    ReleaseCrystalToPool(currentCrystal.gameObject);
                }
                else
                {
                    (player.transform.position, currentCrystal.transform.position) = (currentCrystal.transform.position, player.transform.position);
                    currentCrystal.GetComponent<CrystalSkillController>().ExplodeCrystal();
                }
                
            }

            return true;
        }

        private bool CanUseMultiCrystal()
        {
            if (canUseMultiStacks && multiStackTimer<=0)
            {
                if (currentStacks>0)
                {
                   var crystal = GetCrystalFromPool(player.transform, quaternion.identity);
                   crystal. GetComponent<CrystalSkillController>().SetupCrystal(crystalDuration,canExplode,canMove,moveSpeed,FindClosestEnemy(currentCrystal.transform));
                   currentStacks--;
                   if (currentStacks <= 0)
                   {
                       multiStackTimer = multiStackCooldown;
                       RefillCrystal();
                   }
                }
                
                return true;
            }

            return false;
        }

        private void RefillCrystal()
        {
            currentStacks = amountOfStacks;
        }
        
        private GameObject CreateCrystal()
        {
            var obj =  Instantiate(crystalPrefab,transform);
            obj.SetActive(false);
            return obj;
        }

        private void ActionOnGet(GameObject crystal)
        {
            crystal.gameObject.SetActive(true);
            currentCrystal = crystal;
            //crystalLeft.Remove(crystal);
        }

        private void ActionOnRelease(GameObject crystal)
        {
            crystal.gameObject.SetActive(false);
            if (currentCrystal == crystal)
            {
                currentCrystal = null;
            }
            //crystalLeft.Add(crystal);
        }

        private void ActionOnDestroy(GameObject crystal)
        {
            Destroy(crystal);
        }

        public GameObject GetCrystalFromPool(Transform respawnTarget,Quaternion rotation)
        {
            var obj = crystalPool.Get();
            obj.SetActive(true);
            obj.transform.position = respawnTarget.position;
            obj.transform.rotation = rotation;
            return obj;
        }
        
        public void ReleaseCrystalToPool(GameObject crystal)
        {
            crystalPool.Release(crystal);
        }
    }
}