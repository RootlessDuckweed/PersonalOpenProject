using Enemy.Enemy_BringerOfDeath.SuperState;

namespace Enemy.Enemy_BringerOfDeath.State
{
    public class BringerDeadState : BringerState
    {
        public BringerDeadState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
    }
}