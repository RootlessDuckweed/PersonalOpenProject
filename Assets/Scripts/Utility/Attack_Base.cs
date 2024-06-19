using System;
using UnityEngine;

namespace Utility
{
    public class Attack_Base : MonoBehaviour
    {
        public float damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.GetComponent<Entity>()?.TakeDamage(this.gameObject,damage);
        }
    }
}