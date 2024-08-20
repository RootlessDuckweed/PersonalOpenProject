using UnityEngine;

namespace Enemy.State
{
    public class ArcherJumpBehindSate : ArcherState
    {
        public ArcherJumpBehindSate(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            if(Random.Range(0,100)<50)
                archerEnemy.rb.AddForce(new Vector2(-archerEnemy.facingDir * Random.Range(6,12), Random.Range(6,12)),ForceMode2D.Impulse);
            else
                archerEnemy.rb.AddForce(new Vector2(archerEnemy.facingDir *Random.Range(8,14), Random.Range(8,14)),ForceMode2D.Impulse);
        }

        public override void Update()
        {
            base.Update();
            if (triggerCalled && archerEnemy.CheckGround()||(stateTimer>0.2f && archerEnemy.CheckGround()))
            {
                stateMachine.ChangeState(archerEnemy.crouchAttackSate);
            }
            else if(triggerCalled)
            {
                stateMachine.ChangeState(archerEnemy.jumpBehindNoFallState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}