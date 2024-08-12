using UI.SkillTreeUI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Player.Skill.SpecificSkills.CrystalSkill
{
    public class CrystalSkill : Skill
    {
        [SerializeField] private float crystalDuration;
        [SerializeField] private GameObject crystalPrefab;
        
        [Header("Move")]
        [SerializeField] private float moveSpeed;
        
        [Header("Multi stacking crystal")] 
        [SerializeField] private int amountOfStacks;
        public float multiStackCooldown;
        public float multiStackTimer;
        
        //Skill UI manage
        
        [Header("Regular crystal")] 
        [SerializeField] private UI_SkillTreeSlot crystalUnlockedButton;
        public bool crystalUnlock { get; private set; }

        [Header("Crystal explosion")]
        [SerializeField] private UI_SkillTreeSlot crystalExplosionUnlockedButton;
        public bool crystalExplosionUnlock { get; private set; }
        
        [Header("Crystal controlled destruction")]
        [SerializeField] private UI_SkillTreeSlot crystalControlledUnlockedButton;
        public bool crystalControlledUnlock { get; private set; }
        
        [Header("Crystal multi controlled")]
        [SerializeField]  private UI_SkillTreeSlot crystalMultiControlledUnlockedButton;
        public bool crystalMultiControlledUnlock { get; private set; }

        [Header("Crystal mirage")] 
        [SerializeField] private UI_SkillTreeSlot crystalMirageUnlockButton;
        public bool crystalMirageUnlock { get; private set; }

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
            
            crystalUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockCrystal);
            crystalExplosionUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockExplosion);
            crystalControlledUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockControlled);
            crystalMultiControlledUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockMultiControlled);
            crystalMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalMirage);
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
            if(!crystalMultiControlledUnlock)
                return base.CanUseSkill();
            else
            {
                if (multiStackTimer<=0)
                {
                    UseSkill();
                    return true;
                }

                return false;
            }
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
            if (!crystalUnlock) return false;
            if(crystalMultiControlledUnlock) return false;

            if (currentCrystal==null)
            {
                currentCrystal = GetCrystalFromPool(player.transform,quaternion.identity);
                //currentCrystal = Instantiate(crystalPrefab, player.transform.position, quaternion.identity);
                currentCrystal.GetComponent<CrystalSkillController>().SetupCrystal(crystalDuration,crystalExplosionUnlock,crystalControlledUnlock,moveSpeed,FindClosestEnemy(currentCrystal.transform));
            }
            else
            {

                if (crystalMirageUnlock)
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
            if (crystalMultiControlledUnlock && multiStackTimer<=0)
            {
                if (currentStacks>0)
                {
                   var crystal = GetCrystalFromPool(player.transform, quaternion.identity);
                   crystal. GetComponent<CrystalSkillController>().SetupCrystal(crystalDuration,crystalExplosionUnlock,crystalControlledUnlock,moveSpeed,FindClosestEnemy(currentCrystal.transform));
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

        private void UnlockCrystal()
        {
            if (crystalUnlockedButton.unlocked)
                crystalUnlock = true;
            else
            {
                crystalUnlock = false;
            }
        }

        private void UnlockExplosion()
        {
            if (crystalExplosionUnlockedButton.unlocked)
                crystalExplosionUnlock = true;
            else
            {
                crystalExplosionUnlock = false;
            }
        }

        private void UnlockControlled()
        {
            if (crystalControlledUnlockedButton.unlocked)
                crystalControlledUnlock = true;
            else
            {
                crystalControlledUnlock = false;
            }
        }

        private void UnlockMultiControlled()
        {
            if (crystalMultiControlledUnlockedButton.unlocked)
                crystalMultiControlledUnlock = true;
            else
            {
                crystalMultiControlledUnlock = false;
            }
        }

        private void UnlockCrystalMirage()
        {
            if (crystalMirageUnlockButton.unlocked)
                crystalMirageUnlock = true;
            else
            {
                crystalMirageUnlock = false;
            }
        }
    }
}