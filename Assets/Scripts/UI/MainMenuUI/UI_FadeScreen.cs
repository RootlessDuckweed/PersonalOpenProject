using System;
using UnityEngine;

namespace UI.MainMenuUI
{
    public class UI_FadeScreen : MonoBehaviour
    {
        private Animator anim;
        private static readonly int Out = Animator.StringToHash("FadeOut");
        private static readonly int In = Animator.StringToHash("FadeIn");

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void FadeOut() => anim.SetTrigger(Out);
        public void FadeIn() => anim.SetTrigger(In);
    }
}