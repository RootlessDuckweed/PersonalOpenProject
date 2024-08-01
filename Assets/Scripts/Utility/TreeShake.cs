using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utility
{
    public class TreeShake : MonoBehaviour
    {
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            var state = anim.GetCurrentAnimatorStateInfo(0);
            anim.Play(state.fullPathHash,0,Random.Range(0f,1f));
        }
    }
}