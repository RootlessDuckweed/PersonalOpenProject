using BehaviorDesigner.Runtime.Tasks;

namespace Enemy.ShadyEnemyUsedByBT.BTConditional
{
    public class ShadyEnemyIsWallOrNotGround : ShadyEnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            if (shadyEnemyBt.CheckWall()||!shadyEnemyBt.CheckGround())
            {
                shadyEnemyBt.Flip(); 
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}