using BehaviorDesigner.Runtime.Tasks;

namespace Enemy.ShadyEnemyUsedByBT.BTConditional
{
    public class IsFoundPlayer : ShadyEnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            var hitDash = shadyEnemyBt.DistanceWithPlayer();
            if (hitDash < shadyEnemyBt.dashAttackRadius)
                return TaskStatus.Success;
            
            return TaskStatus.Failure;
            
        }
    }
}