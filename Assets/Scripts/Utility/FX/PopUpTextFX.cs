using System;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Utility.FX.GlobalFXManager;

namespace Utility
{
    public class PopUpTextFX : MonoBehaviour
    {
        private TextMeshPro myText;
        [SerializeField] private float normalSpeed;
        [SerializeField] private float disappearanceSpeed;
        [SerializeField] private float colorDisappearanceSpeed;
        [SerializeField] private float lifeTime;
        private float speed;
        private float textTimer;

        private void Awake()
        {
            myText = GetComponent<TextMeshPro>();
            textTimer = lifeTime;
        }

        private void OnEnable()
        {
            myText.color = Color.white;
            textTimer = lifeTime;
            speed = normalSpeed;
        }

        public void SetupPopText(string text)
        {
            myText.text = text;
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);
            textTimer -= Time.deltaTime;
            if (textTimer < 0)
            {
                float alpha = myText.color.a - colorDisappearanceSpeed * Time.deltaTime;
                myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, alpha);
                if (myText.color.a < 50)
                {
                    speed = disappearanceSpeed;
                }

                if (myText.color.a <= 0.1f)
                {
                    FXGlobalManager.Instance.ReturnToTextPool(this.gameObject);   
                }
                
            }
        }
    }
}