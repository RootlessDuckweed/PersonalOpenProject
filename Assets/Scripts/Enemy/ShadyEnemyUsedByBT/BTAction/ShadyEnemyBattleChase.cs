using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.ShadyEnemyUsedByBT.BTAction
{
    public class ShadyEnemyBattleChase : ShadyEnemyAction
    {
        public override void OnStart()
        {
            shadyEnemyBt.ChangeAnimation("Move");
        }

        public override TaskStatus OnUpdate()
        {
            shadyEnemyBt.FacingPlayer();
            shadyEnemyBt.SetVelocity(shadyEnemyBt.attackMoveSpeed * shadyEnemyBt.facingDir, shadyEnemyBt.rb.velocity.y);
            return TaskStatus.Running;
        }
    }
}