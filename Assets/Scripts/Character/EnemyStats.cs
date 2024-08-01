using Inventory;
using UnityEngine;
using Utility;

namespace Character
{
    public class EnemyStats : CharacterStats
    {
        private Enemy.Enemy _enemy;
        [SerializeField] private int level;
        
        [Range(0f, 1f)] 
        [SerializeField] private float percentageModifier;

        private ItemDrop dropItem;
        protected override void Start()
        {
            LevelModifier(maxHealth);
            LevelModifier(damage);
            base.Start();
            _enemy = GetComponent<Enemy.Enemy>();
            dropItem = GetComponent<ItemDrop>();
        }

        private void LevelModifier(Stat stat)
        {
            for (int i = 1; i < level; i++)
            {
                stat.AddModifier(stat.GetValue()*percentageModifier);
            }
        }
      

        protected override void Die()
        {
            base.Die();
            _enemy.Die();
            dropItem?.GenerateDrop();
        }
    }
}