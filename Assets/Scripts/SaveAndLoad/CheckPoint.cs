using System;
using Player.Universal;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;

namespace SaveAndLoad
{
    public class CheckPoint : MonoBehaviour
    {
        private Animator anim;
        public string checkPointID; 
        private static readonly int Active = Animator.StringToHash("Active");
        [FormerlySerializedAs("activated")] public bool isActivated;
        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() && isActivated == false)
            {
                Activated();
                SaveManager.Instance.SaveGame();
            }
        }

        public void Activated()
        {
            anim.SetBool(Active,true);
            transform.GetChild(0).gameObject.SetActive(true);
            isActivated = true;
        }

        [ContextMenu("GenerateID")]
        private void GenerateID()
        {
            checkPointID = Guid.NewGuid().ToString();
        }
    }
}