using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.ShadyEnemyUsedByBT.BTConditional
{
    public class ShadyEnemyCanDash : ShadyEnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            var hitDash = shadyEnemyBt.DistanceWithPlayer();
            if(hitDash <= shadyEnemyBt.dashAttackRadius && CanEnterDash())
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
        
        private bool CanEnterDash()
        {
            return Time.time > shadyEnemyBt.lastEnterDashTime + shadyEnemyBt.dashCooldown;
        }
    }
}