using System;
using Character;
using Player.Universal;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Inventory.EquipmentItemEffect.IceAndFireEffect
{
    [CreateAssetMenu(fileName = "Item Effect Lightning",menuName = "Inventory/ItemEffect/IceAndFireEffect")]
    public class IceAndFireEffect : ItemEffect
    {
        [SerializeField] private GameObject IceAndFirePrefab;
        [SerializeField] private Vector2 velocity;
        private float lastTime; //如果私有的变量需要将其添加可序列注解 否则这个变量的初始值不可控
        public override void ExecuteNormalEffect(GameObject player = null, GameObject enemy = null)
        {
            base.ExecuteNormalEffect(player, enemy);
        }

        public override void CancelNormalEffect(GameObject player = null, GameObject enemy = null)
        {
            base.CancelNormalEffect(player, enemy);
        }

        private void OnEnable()
        {
            lastTime = 0;
        }

        private void OnDisable()
        {
            lastTime = 0;
        }

        public override void ExecuteSpecialEffect(GameObject player = null, GameObject enemy = null)
        {
            if (Time.time - lastTime >= 1f)
            {
                base.ExecuteSpecialEffect(player, enemy);
                var newIceAndFire = Instantiate(IceAndFirePrefab, player.transform.position, quaternion.identity);
                newIceAndFire.GetComponent<IceAndFireController>().Setup(player.GetComponent<CharacterStats>(), 
                    enemy.transform, velocity, player.GetComponent<Entity>().facingDir);
                MonoBehaviour.print("ExecuteSpecialEffect");
                lastTime = Time.time;
            }
        }

        public override void CancelSpecialEffect(GameObject player = null, GameObject enemy = null)
        {
            base.CancelSpecialEffect(player, enemy);
        }
    }
}