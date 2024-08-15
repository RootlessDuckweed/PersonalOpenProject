using Player.Universal;

namespace Enemy.Enemy_BringerOfDeath
{
    public class DeathBringerAnimationEvent : EnemyAnimationEvent
    {
        private DeathBringerEnemy bringerEnemy;
        protected override void Awake()
        {
            base.Awake();
            bringerEnemy = GetComponentInParent<DeathBringerEnemy>();
        }

        private void Relocate()
        {
            if (bringerEnemy.isTeleportToPlayer)
            {
                bringerEnemy.transform.position = PlayerManager.Instance.Player.transform.position;
                return;
            }
            bringerEnemy.FindPosition();
        }

        private void MakeInvisible() => bringerEnemy.fx.MakeTransparent(true);
        private void MakeVisible()=> bringerEnemy.fx.MakeTransparent(false);
    }
}