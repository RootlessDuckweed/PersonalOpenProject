using UnityEngine;

namespace Enemy.Enemy_Shady.State.SuperState
{
    public class ShadyGroundState : ShadySate
    {
        public ShadyGroundState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
           
        }

        public override void Update()
        {
            base.Update();
            if (shadyEnemy.IsDetectedPlayer()|| Vector2.Distance(shadyEnemy.transform.position,_playerTrans.position)<12)
            {
                stateMachine.ChangeState(shadyEnemy.battleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}