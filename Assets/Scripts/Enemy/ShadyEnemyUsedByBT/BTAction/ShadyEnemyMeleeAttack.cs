using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.ShadyEnemyUsedByBT.BTAction
{
    public class ShadyEnemyMeleeAttack : ShadyEnemyAction
    {
        public override void OnStart()
        {
            shadyEnemyBt.ChangeAnimation("Attack");
            shadyEnemyBt.ZeroVelocity();
        }

        public override TaskStatus OnUpdate()
        {
            if (shadyEnemyBt.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95 &&
                shadyEnemyBt.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) 
            {
                
                //Debug.Log($"完成一次：{shadyEnemyBt.anim.GetCurrentAnimatorStateInfo(0).normalizedTime}");
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Running;
            }
        }
        
    }
}