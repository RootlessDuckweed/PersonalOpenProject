using Player.Universal;
using UnityEngine;

namespace Inventory.FlaskEffect
{
    [CreateAssetMenu(fileName = "Item Heal Flask",menuName = "Inventory/ItemEffect/Heal Flask Effect")]
    public class HealFlaskEffect : ItemEffect
    {
        public float healthAmount;
        public override void ExecuteNormalEffect(GameObject player = null, GameObject enemy = null)
        {
            base.ExecuteNormalEffect(player, enemy);
        }

        public override void CancelNormalEffect(GameObject player = null, GameObject enemy = null)
        {
            base.CancelNormalEffect(player, enemy);
        }

        public override void ExecuteSpecialEffect(GameObject player = null, GameObject enemy = null)
        {
            base.ExecuteSpecialEffect(player, enemy);
            var max = PlayerManager.Instance.Player.stats.GetMaxHealth();
            if (PlayerManager.Instance.Player.stats.currentHealth + healthAmount > max)
            {
                PlayerManager.Instance.Player.stats.currentHealth = max;
            }
            else
            {
                PlayerManager.Instance.Player.stats.currentHealth += healthAmount;
            }
        }

        public override void CancelSpecialEffect(GameObject player = null, GameObject enemy = null)
        {
            base.CancelSpecialEffect(player, enemy);
        }
    }
}