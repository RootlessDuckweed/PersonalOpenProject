using Character;
using UnityEngine;
using Utility;

namespace Inventory.EquipmentItemEffect.IceAndFireEffect
{
    public class IceAndFireController : MonoBehaviour
    {
        public CharacterStats attackerStats;
        private Animator anim;
        private Rigidbody2D rb;
        private static readonly int Hit = Animator.StringToHash("Hit");

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        public void Setup(CharacterStats attacker,Transform target,Vector2 velocity,int facingDir)
        {
            anim.SetTrigger(Hit);
            attackerStats = attacker;
            rb.velocity = velocity*facingDir;
            transform.localScale = new Vector3(facingDir, 1, 1);
            DestroySelf();
        }

        private void DestroySelf()
        {
            Destroy(gameObject,1f);
        }
    }
}