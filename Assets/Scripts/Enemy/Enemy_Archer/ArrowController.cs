using System;
using Character;
using Cinemachine;
using Player.Universal;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private bool worked;
        public Rigidbody2D rb;
        [SerializeField] private int speed;
        [SerializeField] private CapsuleCollider2D cd;
        [SerializeField] private ParticleSystem windEffect;
        [SerializeField] private ParticleSystem trailEffect;
        private string targetLayerName="Player";
        private bool canMove = true;

        private void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.P))
            {
                SetupArrow(PlayerManager.Instance.Player.stats,new Vector2(-1,0));
            }
            if(worked)*/
            if(canMove)
                transform.right = rb.velocity;
            print(targetLayerName);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            
            if (LayerMask.NameToLayer(targetLayerName) == other.gameObject.layer 
                || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                canMove = false;
                Quaternion originalRotation = transform.rotation;
                print(transform.rotation.eulerAngles);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                rb.isKinematic = true;
                windEffect.Stop();
                trailEffect.Stop();
                cd.enabled = false;

                // 将箭矢设为玩家的子物体
                transform.parent = other.transform;

                // 恢复箭矢的全局旋转
                transform.rotation = originalRotation;

                print(transform.rotation.eulerAngles);
                Destroy(gameObject, 2f);
            }
        }

        public void FlipArrow(bool isReturn)
        {
            rb.velocity = -rb.velocity;
            targetLayerName = "Enemy";
            GetComponentInChildren<ArrowAttack>().ChangeTarget();
        }
        public void SetupArrow(CharacterStats stats,Vector2 direction)
        {
            rb.velocity = direction * speed;
            GetComponentInChildren<ArrowAttack>().SetupArrow(stats,cd);
            worked = true;
        }
    }
}