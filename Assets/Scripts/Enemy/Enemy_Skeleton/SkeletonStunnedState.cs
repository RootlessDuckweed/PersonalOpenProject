namespace Enemy.Enemy_Skeleton
{
    public class SkeletonStunnedState : EnemyState
    {
        private SkeletonEnemy _enemy;
        public SkeletonStunnedState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
            if (base.enemyBase is SkeletonEnemy)
            {
                _enemy = base.enemyBase as SkeletonEnemy;
            }
            
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.fx.InvokeRepeating("RedColorBlink",0,.1f);
            _enemy.SetVelocity(-_enemy.facingDir*_enemy.stunnedDirection.x,_enemy.stunnedDirection.y);
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer >= _enemy.stunnedDuration)
            {
                stateMachine.ChangeState(_enemy.idleState);
            }
        }

        public override void Exit()
        { 
            _enemy.fx.Invoke("CancelColorBlink",0);
            base.Exit();
           
        }
    }
}