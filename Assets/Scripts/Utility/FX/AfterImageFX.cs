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

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            orignalColor = sr.color;
        }

        private void OnEnable()
        {
            sr.sprite = PlayerManager.Instance.Player.sr.sprite;
            transform.position = PlayerManager.Instance.Player.transform.position;
            transform.rotation = PlayerManager.Instance.Player.transform.rotation;
            transform.localScale = PlayerManager.Instance.Player.transform.localScale;
            sr.color = orignalColor;
        }

        public void SetupAfterImage(float loosingSpeed)
        {
            colorLooseRate = loosingSpeed;
        }

        private void Update()
        {
            sr.color = new Color(sr.color.r,sr.color.b,sr.color.g,sr.color.a- colorLooseRate * Time.deltaTime);
            if (sr.color.a <= 0.01f)
            {
                PlayerManager.Instance.Player.fx.ReturnToShadowPool(gameObject);
            }
        }
    }
}