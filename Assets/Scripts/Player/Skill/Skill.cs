using Player.Universal;
using UnityEngine;
using Utility.FX.GlobalFXManager;

namespace Player.Skill
{
    public class Skill : MonoBehaviour
    {
        public float coolDown;
        public float coolDownTimer { get; private set; }
        public bool canUse;
        public bool coolDownCompleted;
        protected PlayerController player;

        protected virtual void Start()
        {
            player = PlayerManager.Instance.Player;
        }

        protected virtual void Update()
        {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer <= 0)
                coolDownCompleted = true;
        }
    
        public virtual bool CanUseSkill()
        {
            if (canUse&&coolDownTimer<=0)
            {
                UseSkill();
                coolDownTimer = coolDown;
                coolDownCompleted = false;
                return true;
            }
            
            FXGlobalManager.Instance.CreatePopUpText("CoolDown!",player.transform);
            return false;
        }

        public virtual void UseSkill()
        {
            // do some skill logical 
            // specific logical is performed by inheritors
           
        }

        protected virtual Transform FindClosestEnemy(Transform skillController)
        {
            var colliders = Physics2D.OverlapCircleAll(skillController.position, 20);

            float closestDistance = Mathf.Infinity;
            Transform closestEnemy = null;
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy.Enemy>() != null)
                {
                    var distance = Vector2.Distance(skillController.position, hit.transform.position);
                    if (distance < closestDistance)
                    {
                        closestEnemy = hit.transform;
                        closestDistance = distance;
                    }
                }
            }

            return closestEnemy;
        }
    }
}                                    