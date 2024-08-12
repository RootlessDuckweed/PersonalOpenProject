using Inventory;
using Player;
using Player.Universal;
using UnityEngine.Events;
using UI;
using UnityEngine;

namespace Character
{
    public class PlayerStats : CharacterStats
    {
        private PlayerController player;
        public UnityAction OnStatsChanged;
        [SerializeField] private StatUI statUI;

        protected override void Awake()
        {
            base.Awake();
            OnStatsChanged += statUI.UpdateValueUI;
        }
        

        protected override void Start()
        {
            base.Start();
            player = PlayerManager.Instance.Player;
        }

        protected override void Die()
        {
            base.Die();
            player.Die();
            GetComponent<PlayerItemDrop>()?.GenerateDrop();
        }
    }
}