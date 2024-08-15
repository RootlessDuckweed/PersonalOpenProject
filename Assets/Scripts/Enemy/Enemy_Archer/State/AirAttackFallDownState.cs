using Enemy.State;

namespace Enemy.Enemy_Archer.State
{
    public class AirAttackFallDownState : ArcherState
    {
        public AirAttackFallDownState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }
    }
}