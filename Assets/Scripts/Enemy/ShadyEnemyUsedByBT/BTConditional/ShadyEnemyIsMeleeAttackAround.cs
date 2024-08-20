using BehaviorDesigner.Runtime.Tasks;

namespace Enemy.ShadyEnemyUsedByBT.BTConditional
{
    public class ShadyEnemyIsMeleeAttackAround : ShadyEnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            if (shadyEnemyBt.DistanceWithPlayer() <= shadyEnemyBt.attackDistance+1.5f)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}