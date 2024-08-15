using Enemy.Enemy_Shady.State.SuperState;
using Player.Universal;
using UnityEngine;

namespace Enemy.Enemy_Shady.State
{
    public class ShadySpurtState : ShadySate
    {
        private Vector3 dir;
        private Vector2 targetPosition;
        private float speed = 70f;
        public ShadySpurtState(EnemyStateMachine _stateMachine, Enemy enemyBase, string _animBoolName) : base(_stateMachine, enemyBase, _animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            targetPosition = new Vector2(_playerTrans.position.x + (PlayerManager.Instance.Player.facingDir),
                _playerTrans.position.y
            );
            Debug.Log("Surat");
        }

        public override void Update()
        {
            base.Update();
            shadyEnemy.FacingPlayer();
            shadyEnemy.GenerateShadow();
            shadyEnemy.transform.position = Vector2.MoveTowards(shadyEnemy.transform.position, targetPosition,
                speed * Time.deltaTime);
            if (Vector2.Distance(shadyEnemy.transform.position, targetPosition) <=0.1f)
            {
                stateMachine.ChangeState(shadyEnemy.attackState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}