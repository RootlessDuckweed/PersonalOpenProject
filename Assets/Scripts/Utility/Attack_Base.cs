using System;
using UnityEngine;

namespace Utility
{
    public class Attack_Base : MonoBehaviour
    {
        public float damage;

        protected virtual void  OnTriggerEnter2D(Collider2D other)
        {
            
            // take damage
            Enemy.Enemy enemy= other.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(this.gameObject,damage,enemy.isFrozenTime);
                return;
            }
            other.GetComponent<Entity>()?.TakeDamage(gameObject,damage,false);
            
        }
        
    }
}