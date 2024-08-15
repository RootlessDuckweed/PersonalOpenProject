using UnityEngine;

namespace Enemy.Enemy_BringerOfDeath.SuperState
{
    public class BringerGroundState : BringerState
    {
        public BringerGroundState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
           
        }

        public override void Update()
        {
            base.Update();
            if (bringerEnemy.IsDetectedPlayer()|| Vector2.Distance(bringerEnemy.transform.position,_playerTrans.position)<6)
            {
                stateMachine.ChangeState(bringerEnemy.battleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}