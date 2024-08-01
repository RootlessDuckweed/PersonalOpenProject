using Character;
using UnityEngine;

namespace Inventory.EquipmentItemEffect.ThunderStrikeEffect
{
    [CreateAssetMenu(fileName = "Item Effect Lightning",menuName = "Inventory/ItemEffect/ThunderStrikeEffect")]
    public class ThunderStrikeEffect : ItemEffect
    {
        public GameObject ThunderStrikePrefab;
        private float lastTime;
        private void OnEnable()
        {
            lastTime = 0;
        }

        private void OnDisable()
        {
            lastTime = 0;
        }
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
            if (Time.time - lastTime >= 0.001f)
            {
                base.ExecuteSpecialEffect(player, enemy);
                var newThunderStrike = Instantiate(ThunderStrikePrefab);
                newThunderStrike.GetComponent<ThunderBigStrikeController>()
                    .Setup(player.GetComponent<CharacterStats>(), enemy.transform);
                lastTime = Time.time;
            }
        }

        public override void CancelSpecialEffect(GameObject player = null, GameObject enemy = null)
        {
            base.CancelSpecialEffect(player, enemy);
        }
    }
}