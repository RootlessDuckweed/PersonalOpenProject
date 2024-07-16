using System;
using UnityEngine;

namespace Player.Skill.SpecificSkills
{
    public class CrystalSkillController : MonoBehaviour
    {
        private float _crystalExistTimer = Mathf.Infinity;
        private bool _canExplode;
        private bool _canMove;
        private float _moveSpeed;
        private bool _canGrow;
        private float _growSpeed;
        private Transform _closestEnemy;
        private static readonly int Explode = Animator.StringToHash("Explode");
        private Animator anim => GetComponent<Animator>();

        public void SetupCrystal(float crystalExistDuration,bool canExplode,bool canMove,float moveSpeed,Transform closestEnemy,bool canGrow=true,float growSpeed=5)
        {
            _crystalExistTimer = crystalExistDuration;
            _canExplode = canExplode;
            _canMove = canMove;
            _moveSpeed = moveSpeed;
            _closestEnemy = closestEnemy;
            _canGrow = canGrow;
            _growSpeed = growSpeed;

        }

        private void Update()
        {
            _crystalExistTimer -= Time.deltaTime;
            if (_crystalExistTimer < 0)
            {
                ExplodeCrystal();
            }

            if (_canMove)
            {
                transform.position = Vector2.MoveTowards(transform.position, _closestEnemy.position,
                    _moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, _closestEnemy.position) <= 1)
                {
                    ExplodeCrystal();
                }
            }
                
            
            if (_canGrow)
            {
                transform.localScale =
                    Vector2.Lerp(transform.localScale, new Vector3(2, 2), _growSpeed * Time.deltaTime);
            }
            
        }

       public void ExplodeCrystal()
        {
            if (_canExplode)
            {
                _canMove = false;
                anim.SetTrigger(Explode);
            }
            else
            {
                SelfDestroy();
            }
        }
       
       
        private void SelfDestroy()
        {
            SkillManager.Instance.crystalSkill.ReleaseCrystalToPool(this.gameObject);
        }
    }
}