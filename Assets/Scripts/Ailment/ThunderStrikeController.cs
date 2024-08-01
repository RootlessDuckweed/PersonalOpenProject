using Character;
using UnityEngine;


namespace Ailment
{
    public class ThunderStrikeController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;
        private Animator anim;
        private bool triggered;
        private static readonly int Hit = Animator.StringToHash("Hit");
        public CharacterStats attacker;
        public float damage;
        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
        }

        public void Setup(CharacterStats attacker,Transform target,float damage)
        {
            if(target == null) return;
            this.target = target;
            this.damage = damage;
            this.attacker = attacker;
        }
        
        private void Update()
        {
            if (!target)
            {
                triggered = true;
                anim.SetTrigger(Hit);
                Destroy(gameObject,.4f);
            }  
            if(triggered) return;

            transform.position =
                Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.right = transform.position -  target.position;
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                anim.transform.localRotation = Quaternion.identity;
                transform.localRotation = Quaternion.identity;
                transform.localScale = new Vector3(2, 2, 2);
                
                triggered = true;
                anim.SetTrigger(Hit);
                Destroy(gameObject,.4f);
            }
        }
    }
}