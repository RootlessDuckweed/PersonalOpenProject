using Character;
using UnityEngine;
using Utility;

namespace Enemy
{
    public class EnemyAttack : Attack_Base
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (stat == null)
            {
                stat = GetComponentInParent<CharacterStats>();
            }
            base.OnTriggerEnter2D(other);
        }
        
    }
}