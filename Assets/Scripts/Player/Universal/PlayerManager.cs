using SaveAndLoad;
using UnityEngine;
using Utility;

namespace Player.Universal
{
    public class PlayerManager : Singleton<PlayerManager>,ISaveManager
    {
        public PlayerController Player { get; private set; }

        public int currency;
        protected override void Awake()
        {
            base.Awake();
            Player = GameObject.FindFirstObjectByType<PlayerController>();
        }

        public bool HaveEnoughMoney(int price)
        {
            if (price > currency)
            {
                return false;
            }

            currency -= price;
            return true;
        }

        public void LoadData(GameData data)
        {
            currency = data.currency;
        }

        public void SaveData(ref GameData data)
        {
            data.currency = currency;
        }
    }
}