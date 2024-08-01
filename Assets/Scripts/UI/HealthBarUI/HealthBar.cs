using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.HealthBarUI
{
    public class HealthBar : MonoBehaviour
    {
        private Entity entity;
        private RectTransform rectTransform;
        private Slider slider;
        private void Awake()
        {
            entity = GetComponentInParent<Entity>();
            rectTransform = GetComponent<RectTransform>();
            slider = GetComponentInChildren<Slider>();
        }

        private void OnEnable()
        {
            entity.onFlipped.AddListener(OnEntityFlipped);
            entity.stats.onDead.AddListener(OnEntityDead);
           
        }

        private void OnDisable()
        {
            entity.onFlipped.RemoveListener(OnEntityFlipped);
            entity.stats.onDead.RemoveListener(OnEntityDead);
        }

        private void Update()
        {
            float healthPercent = entity.stats.currentHealth / entity.stats.GetMaxHealth();
            if(slider.value.Equals(healthPercent) || slider.value < healthPercent+0.01f && slider.value>healthPercent-0.01f) return;
            if (slider.value > healthPercent)
            {
                slider.value -= Time.deltaTime;
            }
            else
            {
                slider.value += Time.deltaTime;
            }
        }

        private void OnEntityDead()
        {
            gameObject.SetActive(false);
        }
        
        private void OnEntityFlipped()
        {
            rectTransform.Rotate(0,180,0);
        }
    }
}