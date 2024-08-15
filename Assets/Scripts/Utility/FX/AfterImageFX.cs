using System;
using Player.Universal;
using UnityEngine;

namespace Utility
{
    public class AfterImageFX : MonoBehaviour
    {
        private SpriteRenderer sr;
        private float colorLooseRate;
        private Color orignalColor;
        [SerializeField] private Entity whose;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            orignalColor = sr.color;
        }

        private void OnEnable()
        {
            sr.sprite = whose.sr.sprite;
            transform.position = whose.transform.position;
            transform.rotation = whose.transform.rotation;
            transform.localScale = whose.transform.localScale;
            sr.color = orignalColor;
        }

        public void SetupAfterImage(float loosingSpeed,Entity entity)
        {
            colorLooseRate = loosingSpeed;
            whose = entity;
        }

        private void Update()
        {
            sr.color = new Color(sr.color.r,sr.color.b,sr.color.g,sr.color.a- colorLooseRate * Time.deltaTime);
            if (sr.color.a <= 0.01f)
            {
                whose.fx.ReturnToShadowPool(gameObject);
            }
        }
    }
}