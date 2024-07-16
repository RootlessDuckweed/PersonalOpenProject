using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public PlayerController Player { get; private set; } 

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Player = GameObject.FindFirstObjectByType<PlayerController>();
                Debug.Log(Player.name);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}