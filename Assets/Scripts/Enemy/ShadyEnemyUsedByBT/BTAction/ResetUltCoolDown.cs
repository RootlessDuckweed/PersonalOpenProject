using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.ShadyEnemyUsedByBT.BTAction
{
    public class ResetUltCoolDown : ShadyEnemyAction
    {
        public override void OnStart()
        {
            base.OnStart();
            shadyEnemyBt.lastEnterDashTime = Time.time;
        }
        
    }
}