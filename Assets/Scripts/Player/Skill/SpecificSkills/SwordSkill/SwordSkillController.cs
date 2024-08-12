using System.Collections.Generic;
using Player.Universal;
using UnityEngine;

namespace Player.Skill.SpecificSkills.SwordSkill
{
    public class SwordSkillController : MonoBehaviour
    {
        private Animator anim;
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        private PlayerController player;
        private bool canRotate = true;
        private bool isReturning;
        private float returnSpeed;

        [Header("Pierce info")] 
        private int pierceAmount;
        
        [Header("Bounce info")]
        private float bounceSpeed;
        private bool isBouncing ;
        private int amountOfBounce;
        private List<Transform> enemyTarget;
        private int targetIndex;

        [Header("Spin nifo")] 
        private float maxTravelDistance;
        private float spinDuration;
        private float spinTimer;
        private bool wasStopped;
        private bool isSpinning;
        private Vector2 spinDiretion;
        private float freezeTimeDuration;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CircleCollider2D>();
            enemyTarget = new List<Transform>();
        }

        private void Start()
        {
            player = PlayerManager.Instance.Player;
        }

        private void DestroyMe()
        {
            Destroy(gameObject);
        }
        public void SetupSword(Vector2 finalDir, float gravity,float returnSpeed)
        {
            rb.gravityScale = gravity;
            rb.velocity = finalDir;
            this.returnSpeed = returnSpeed;
            if(SkillManager.Instance.swordSkill.swordType!=SwordType.Pierce)
                anim.SetBool("Rotation",true);
            Invoke("ReturnSword",7);
        }

        public void SetupBounce(bool isBouncing, int amountOfBounce ,float bounceSpeed)
        {
            this.isBouncing = isBouncing;
            this.amountOfBounce = amountOfBounce;
            this.bounceSpeed = bounceSpeed;
        }

        public void SetupPierce(int pierceAmount)
        {
            this.pierceAmount = pierceAmount;
        }

        public void SetupSpin(bool isSpinning,float maxTravelDistance,float spinDuration,Vector2 spinDiretion)
        {
            this.maxTravelDistance = maxTravelDistance;
            this.isSpinning = isSpinning;
            this.spinDuration = spinDuration;
            this.spinDiretion = spinDiretion;
        }
        
        private void FixedUpdate()
        {
            if(canRotate)
                transform.right = rb.velocity;
            if(isReturning)
            { 
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                                    returnSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, player.transform.position) < 2)
                {
                    player.CatchTheSword();
                }
            }

            BounceUpdateLogic();

            SpinUpdateLogic();
        }

        private void SpinUpdateLogic()
        {
            if (isSpinning)
            {
                if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
                {
                    wasStopped = true;
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    spinTimer = spinDuration;
                }

                if (wasStopped)
                {
                    spinTimer -= Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDiretion.x, transform.position.y+ spinDiretion.y),1.5f*Time.deltaTime);
                    if (spinTimer < 0)
                    {
                        isReturning = true;
                        isSpinning = false;
                    }
                }
            }
        }

        private void BounceUpdateLogic()
        {
            if (isBouncing && enemyTarget.Count > 0)
            {
                /*Debug.Log("bouncing");*/
                transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position,
                    bounceSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
                {
                    targetIndex++;
                    amountOfBounce--;
                    if (amountOfBounce <= 0)
                    {
                        isBouncing = false;
                        isReturning = true;
                        return;
                    }
                    if (targetIndex >= enemyTarget.Count)
                    {
                        targetIndex = 0;
                    }
                }
            }
            
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag.Equals("Water")) return;
            if(isReturning)
                return;
            
            if (SkillManager.Instance.swordSkill.swordType == SwordType.Bounce)
            {
                if (BouncingSwordLogic(other)) return;
            }
            else if(SkillManager.Instance.swordSkill.swordType == SwordType.Pierce)
            {
                if (PierceSwordLogic(other)) return;
            }
            else if (SkillManager.Instance.swordSkill.swordType == SwordType.Spin)
            {
                return;
            }
            StuckInto(other);
        }

        private bool PierceSwordLogic(Collider2D other)
        {
            if (pierceAmount > 0 && other.GetComponent<Enemy.Enemy>() != null)
            {
                pierceAmount--;
                return true;
            }

            return false;
        }

        private bool BouncingSwordLogic(Collider2D other)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            if (other.GetComponent<Enemy.Enemy>() != null)
            {
                if (isBouncing && enemyTarget.Count <= 0)
                {
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10,LayerMask.GetMask("Enemy"));
                    foreach (var hit in colliders)
                    {
                        enemyTarget.Add(hit.transform);
                    }
                    enemyTarget.Sort((x, y) => 
                        (x.position - player.transform.position).magnitude.CompareTo(
                            (y.position - player.transform.position).magnitude)
                    );
                }

                return true;
            }

            return false;
        }

        private void StuckInto(Collider2D other)
        {
            var enemy = other.GetComponent<Enemy.Enemy>();
            if(enemy!=null && enemy.stats.IsEvasion()) return;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            amountOfBounce = 0;
            enemyTarget.Clear();
            canRotate = false;
            cd.enabled = false;
            anim.SetBool("Rotation",false);
            transform.parent = other.transform;
        }

        public void ReturnSword()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //rb.isKinematic = false;
            transform.parent = null;
            isReturning = true;
            print("is Returning");
        }
    }
}