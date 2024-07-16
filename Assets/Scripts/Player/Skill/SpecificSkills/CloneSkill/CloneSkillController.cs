using System;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Player.Skill.SpecificSkills
{
    public class CloneSkillController : MonoBehaviour
    {
        private SpriteRenderer _sr;
        private Animator _anim;
        private float _colorLoosingSpeed;
        private float _cloneTimer;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
        }
        
        
        private void Update()
        {
            _cloneTimer -= Time.deltaTime;
            if (_cloneTimer < 0)
            {
                _sr.color = new Color(1, 1, 1, _sr.color.a - (Time.deltaTime * _colorLoosingSpeed));
            }

            if (_sr.color.a <= 0f)
            {
                _sr.color = new Color(1, 1, 1, 1);
                SkillManager.Instance.cloneSkill.ReleaseCloneObj(this.gameObject);
            }
        }

        #region Animation Called

        public void AnimationFinished()
        {
            _cloneTimer = -1;
        }
        
        #endregion
        
        public void SetupClone(Entity target,float cloneDuration,float colorLoosingSpeed,Vector3 offset = default)
        {
            
            transform.position = target.transform.position+offset;
            float dir = target.facingDir;
            if (offset.x > 0&&dir > 0)
            {
                dir = -dir;
            }else if (offset.x < 0)
            {
                dir = 1;
            }
            transform.localScale = new Vector3(dir, 1, 1);
            _cloneTimer = cloneDuration;
            this._colorLoosingSpeed = colorLoosingSpeed;
            _anim.SetInteger("AttackNumber",Random.Range(1,4));
        }
    }
}