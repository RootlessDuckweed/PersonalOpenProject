using Player.Universal;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.InGameUI.HealthBarUI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        private Entity entity;
        private Slider slider;
       
        protected  void Awake()
        {
            entity =PlayerManager.Instance.Player.GetComponent<Entity>();
            slider = GetComponent<Slider>();
        }

      

        
        private  void Update()
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

       
    }
}