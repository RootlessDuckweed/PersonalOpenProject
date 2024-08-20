using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.ShadyEnemyUsedByBT.BTAction
{
    public class ShadyEnemySpurt : ShadyEnemyAction
    {
        private Vector3 dir;
        private Vector2 targetPosition;
        private float speed = 70f;
        public SharedFloat defaultGravity;
        public override void OnAwake()
        {
            base.OnAwake();
            defaultGravity.Value = shadyEnemyBt.rb.gravityScale;
        }

        public override void OnStart()
        {
            shadyEnemyBt.ChangeAnimation("Dash");
            targetPosition = new Vector2(player.transform.position.x + (player.facingDir),
                player.transform.position.y
            );
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