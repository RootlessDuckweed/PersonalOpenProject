using System;
using Enemy.Enemy_BringerOfDeath.State;
using Player.Universal;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy.Enemy_BringerOfDeath
{
    public class DeathBringerEnemy : Enemy
    {
        //todo:死亡逻辑后面写
        [Header("Teleport details")]
        [SerializeField] private BoxCollider2D arena;
        [SerializeField] private Vector2 surroundingCheckSize;
        public float chanceToTeleport;
        public float defaultChanceToTeleport=25f;

        [Header("Spell Cast details")] 
        [SerializeField] private GameObject spellPrefab;
        [SerializeField] private float spellCastCooldown;
        public float lastCastTime;
        public int amountsOfSpells;
        public float eachSpellCooldown;
        public bool isTeleportToPlayer;
        public bool isCooldown;
        public BringerIdleState idleState { get; private set; }
        public BringerMoveState moveState { get; private set; }
        public BringerBattleState battleState { get; private set; }
        public BringerAttackBusyState attackBusyState { get; private set; }
        public BringerNormalAttackState attackState{ get; private set; }
        public BringerSpellCastState spellCastState{ get; private set; }
        public BringerTeleportState teleportState { get; private set; }
        private void Start()
        {
            idleState = new BringerIdleState(stateMachine, this, "Idle");
            moveState = new BringerMoveState(stateMachine, this, "Move");
            attackBusyState = new BringerAttackBusyState(stateMachine, this, "Idle");
            battleState = new BringerBattleState(stateMachine, this, "Move");
            attackState = new BringerNormalAttackState(stateMachine, this, "Attack");
            spellCastState = new BringerSpellCastState(stateMachine, this, "SpellCast");
            teleportState = new BringerTeleportState(stateMachine, this, "Teleport");
            stateMachine.Initialize(idleState);
            chanceToTeleport=defaultChanceToTeleport;
        }

        protected override void Update()
        {
            base.Update();
            isCooldown = !CanDoSpellCast();
        }

        public void FindPosition()
        {
            float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x-3);
            float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y-3);
            var randomPos = new Vector3(x, y);
            var detailPos = new Vector3(x, y, -GroundBelow(randomPos).distance + (cd.size.y/ 2));
            if (!GroundBelow(detailPos) || SomethingIsAround(detailPos))
            {
                FindPosition();
            }
            else
            {
                transform.position = detailPos;
            }
        }

        private RaycastHit2D GroundBelow(Vector3 target) => Physics2D.Raycast(target, Vector2.down, 5, whatIsGround);

        private bool SomethingIsAround(Vector3 target) =>
            Physics2D.BoxCast(target, surroundingCheckSize, 0, Vector2.zero, 0, whatIsGround);

        public bool CanTeleport()
        {
            if (!(Random.Range(0, 100) < chanceToTeleport)) return false;
            chanceToTeleport = defaultChanceToTeleport;
            return true;

        }

        public bool CanDoSpellCast()
        {
            if (Time.time >= lastCastTime+spellCastCooldown)
            {
                return true;
            }
            
            return false;
        }

        public GameObject CastSpell()
        {
            var player = PlayerManager.Instance.Player;
            float yOffset;
            if (player.rb.velocity.y >= player.rb.gravityScale+1)
            {
                yOffset = 0.5f;
            }
            else if(player.rb.velocity.y==0)
            {
                yOffset = 0;
            }
            else
            {
                yOffset = -0.5f;
            }

            float xOffset;
            if (player.rb.velocity.x >= player.moveSpeed)
            {
                xOffset = 1f;
            }
            else if(player.rb.velocity.x==0)
            {
                xOffset = 0;
            }
            else
            {
                xOffset = -1f;
            }
            var spellPosition = new Vector3(player.transform.position.x + xOffset,
                player.transform.position.y+yOffset);
            var newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
            newSpell.GetComponentInChildren<SpellCastController>().SetupSpellCast(stats);
            return newSpell;
        }
        public float DistanceWithPlayer()
        {
            return Vector2.Distance(transform.position, PlayerManager.Instance.Player.transform.position);
        }
        
        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y-GroundBelow(transform.position).distance));
            Gizmos.DrawWireCube(transform.position,surroundingCheckSize);
        }
    }
}