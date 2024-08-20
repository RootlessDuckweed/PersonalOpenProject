using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.ShadyEnemyUsedByBT.BTAction
{
    public class ShadyEnemySpurtRandom : ShadyEnemyAction
    {
        private Vector2 targetPosition;
        private float speed = 70f;
        public override void OnStart()
        {
            shadyEnemyBt.ChangeAnimation("Dash");
            var x =Random.Range(shadyEnemyBt.dashLimitedLeftDownPoint.position.x, shadyEnemyBt.dashLimitedRightUpPoint.position.x);
            var y=Random.Range(shadyEnemyBt.dashLimitedLeftDownPoint.position.y, shadyEnemyBt.dashLimitedRightUpPoint.position.y );
            targetPosition = new Vector2(x, y);
        }
        
        public override TaskStatus OnUpdate()
        {
            shadyEnemyBt.FacingPlayer();
            shadyEnemyBt.GenerateShadow();
            shadyEnemyBt.transform.position = Vector2.MoveTowards(shadyEnemyBt.transform.position, targetPosition,
                speed * Time.deltaTime);
            if (Vector2.Distance(shadyEnemyBt.transform.position, targetPosition) <=0.1f)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
    }
}