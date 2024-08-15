using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Enemy.Enemy_Shady.State.SuperState
{
    public class ShadyDashState :  ShadySate
    {
        private Vector3 dir;
        private Vector2 targetPosition;
        private float speed = 50f;
        private bool canDash = false;
        public ShadyDashState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            canDash = true;
            var x =Random.Range(shadyEnemy.dashLimitedLeftDownPoint.position.x, shadyEnemy.dashLimitedRightUpPoint.position.x);
            var y=Random.Range(shadyEnemy.dashLimitedLeftDownPoint.position.y, shadyEnemy.dashLimitedRightUpPoint.position.y );
            targetPosition = new Vector2(x, y);
        }

        public override void Update()
        {
            base.Update();
            if (canDash)
            {
                shadyEnemy.FacingPlayer();
                shadyEnemy.GenerateShadow();
                shadyEnemy.transform.position = Vector2.MoveTowards(shadyEnemy.transform.position, targetPosition,
                    speed * Time.deltaTime);
                if (Vector2.Distance(shadyEnemy.transform.position, targetPosition) <=0.1f)
                {
                    stateMachine.ChangeState(shadyEnemy.dashAttackState);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            canDash = false;
        }

        
    }
}