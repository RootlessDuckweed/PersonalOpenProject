using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Utility
{
    public class EmissionWithSprite : MonoBehaviour
    {
       [SerializeField] private Light2D light2D;
       [SerializeField] private SpriteRenderer sr;
       

        private void Update()
        {
            light2D.lightCookieSprite = sr.sprite;
        }
    }
}