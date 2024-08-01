using UnityEngine;
using Utility;

namespace Player.Universal
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public PlayerController Player { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Player = GameObject.FindFirstObjectByType<PlayerController>();
        }
    }
}