using Character;
using UnityEngine;

namespace Enemy.Enemy_Shady
{
    public class ShadyShootController : MonoBehaviour
    {
        public Rigidbody2D rb;
        [SerializeField] private int speed;

        private void Update()
        {
            transform.right = rb.velocity;
        }
        
        
        public void SetupShadyShoot(CharacterStats stats,Vector2 direction)
        {
            rb.velocity = direction * speed;
            GetComponentInChildren<ShadyShootAttack>().SetupShadyShoot(stats);
            Destroy(gameObject,5f);
        }
        
    }
}