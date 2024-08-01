using Inventory;
using Player;
using Player.Universal;

namespace Character
{
    public class PlayerStats : CharacterStats
    {
        private PlayerController player;
        protected override void Start()
        {
            base.Start();
            player = PlayerManager.Instance.Player;
        }

        protected override void Die()
        {
            base.Die();
            player.Die();
            GetComponent<PlayerItemDrop>()?.GenerateDrop();
        }
    }
}