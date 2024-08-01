using System.Collections.Generic;
using Player.Universal;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Player.Skill.SpecificSkills.BlackHoleSkill
{
    public class BlackHoleSkillController : MonoBehaviour
    {
        [SerializeField] private GameObject hotKeyPrefab;
        [SerializeField] private List<KeyCode> keyCodeList;
        
        private float maxSize;
        private float growSpeed;
        private float shrinkSpeed;
        private float cloneAttackCooldown = 0.3f;
        private float cloneAttackTimer;
        private int amountOfAttacks = 4;
        
        private bool canShrink;
        private bool canCreateHotKeys = true;
        private bool canGrow;
        private bool isCompletedGrowth;
        private bool cloneAttackReleased;
        
        private List<Enemy.Enemy> attackedTargets = new List<Enemy.Enemy>();
        private List<GameObject> hotKeys = new List<GameObject>();
        private List<Enemy.Enemy> frozenTargets = new List<Enemy.Enemy>();
        private void Update()
        {
            cloneAttackTimer -= Time.deltaTime;
            if (Keyboard.current.rKey.isPressed && !cloneAttackReleased)
            {
                DestroyHotKey();
                cloneAttackReleased = true;
                canCreateHotKeys = false;
                PlayerManager.Instance.Player.MakeTransparent(true);
            }
            GenerateCloneAttackedLogic();
            
            GrowthLogic();

            ShrinkLogic();
        }

        public void SetupBlackHole(float _maxSize,float _growSpeed,float _shrinkSpeed,int _amountOfAttacks,float _cloneAttackCooldown)
        {
            maxSize = _maxSize;
            growSpeed = _growSpeed;
            shrinkSpeed = _shrinkSpeed;
            amountOfAttacks = _amountOfAttacks;
            cloneAttackCooldown = _cloneAttackCooldown;
            canGrow = true;
        }
        
        private void GenerateCloneAttackedLogic()
        {
            if (cloneAttackTimer < 0 && cloneAttackReleased )
            {
                cloneAttackTimer = cloneAttackCooldown;
                int randomIndex = Random.Range(0, attackedTargets.Count);
                
                float xOffset;
                if (Random.value > 0.5f)
                {
                    xOffset = 1.5f;
                }
                else
                {
                    xOffset = -1.5f;
                }

                if (attackedTargets.Count > 0 && amountOfAttacks > 0)
                {
                    SkillManager.Instance.cloneSkill.UseSkillAndSetPosition(attackedTargets[randomIndex],new Vector3(xOffset,0));
                    amountOfAttacks--; 
                    return;
                }
                canShrink = true;
                PlayerManager.Instance.Player.MakeTransparent(false);
            }
            
        }

        private void ShrinkLogic()
        {
            if (canShrink)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1),
                    shrinkSpeed * Time.deltaTime);
                if (transform.localScale.x < 0)
                {
                    for (int i = 0; i < frozenTargets.Count; i++)
                    {
                        frozenTargets[i].FreezeTime(false);
                    }
                    PlayerManager.Instance.Player.ExitBlackHoleState();
                    Destroy(gameObject);
                }
            }
        }

        private void GrowthLogic()
        {
            if (canGrow&&!canShrink)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize),
                    growSpeed * Time.deltaTime);
                if (transform.localScale.x > maxSize - 0.5f)
                {
                    isCompletedGrowth = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(canShrink||isCompletedGrowth) return;
            var enemy= other.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                SetupHotKey(other, enemy);
                frozenTargets.Add(enemy);
            }
        }

        private void SetupHotKey(Collider2D other, Enemy.Enemy enemy)
        {
            if (keyCodeList.Count <= 0) return;
            if(!canCreateHotKeys) return;

            CreateHotKey(other, enemy);
        }

        private void DestroyHotKey()
        {
            if(hotKeys.Count<=0) return;
            for (int i = 0; i < hotKeys.Count; i++)
            {
                Destroy(hotKeys[i]);
            }
        }

        private void CreateHotKey(Collider2D other, Enemy.Enemy enemy)
        {
            enemy.FreezeTime(true);
            GameObject newHotKey = Instantiate(hotKeyPrefab, other.transform.position + Vector3.up * 2,
                quaternion.identity);
            if (hotKeys.Contains(newHotKey))
            {
                Destroy(newHotKey);
                return;
            }
            KeyCode chosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
            keyCodeList.Remove(chosenKey);
            BlackHoleHotKeyController hotKeyScript = newHotKey.GetComponent<BlackHoleHotKeyController>();
            hotKeyScript.SetupHotKey(chosenKey,enemy,this);
            hotKeys.Add(newHotKey);
        }

        public void AddEnemyToList(Enemy.Enemy enemy) => attackedTargets.Add(enemy);
        
    }
}