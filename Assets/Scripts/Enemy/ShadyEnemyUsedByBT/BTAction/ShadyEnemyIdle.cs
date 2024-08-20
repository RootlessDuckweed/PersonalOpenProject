using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.ShadyEnemyUsedByBT.BTAction
{
    public class ShadyEnemyIdle : ShadyEnemyAction
    {
        private float currentTimer = 0;
        public override void OnStart()
        {
            shadyEnemyBt.ChangeAnimation("Idle");
            currentTimer = 0;
        }

        public override TaskStatus OnUpdate()
        {
            currentTimer += Time.deltaTime;
            if (currentTimer <= shadyEnemyBt.idleTime)
            {
                return TaskStatus.Running;
            }
            
            return TaskStatus.Success;
            
        }
        
    }
}