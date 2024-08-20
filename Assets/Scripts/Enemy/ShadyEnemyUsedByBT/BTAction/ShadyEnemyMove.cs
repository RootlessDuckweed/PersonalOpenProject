using BehaviorDesigner.Runtime.Tasks;

namespace Enemy.ShadyEnemyUsedByBT.BTAction
{
    public class ShadyEnemyMove : ShadyEnemyAction
    {
        public override void OnStart()
        {
            base.OnStart();
            shadyEnemyBt.ChangeAnimation("Move");
        }

        public override TaskStatus OnUpdate()
        {
            shadyEnemyBt.SetVelocity(shadyEnemyBt.moveSpeed*shadyEnemyBt.facingDir,shadyEnemyBt.rb.velocity.y);
            return TaskStatus.Running;
        }
    }
}