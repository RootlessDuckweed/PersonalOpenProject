using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Utility
{
    public class LightIncreasesWithTime : MonoBehaviour
    {
        private Light2D light2D => GetComponent<Light2D>();
        [SerializeField] private float targetIntensity;
        [SerializeField] private float increasedSpeedIntensity;
        [SerializeField] private float outer;
        [SerializeField] private float increasedSpeedOuter;
        [SerializeField] private float inner;
        [SerializeField] private float increasedSpeedInner;
      
        private void Update()
        {
            
                light2D.intensity = Mathf.Lerp(light2D.intensity, targetIntensity, increasedSpeedIntensity * Time.deltaTime);
                light2D.pointLightOuterRadius = Mathf.Lerp(light2D.pointLightOuterRadius,outer , increasedSpeedOuter * Time.deltaTime);
                light2D.pointLightInnerRadius = Mathf.Lerp(light2D.pointLightInnerRadius,inner, increasedSpeedInner * Time.deltaTime);

        }
    }
}