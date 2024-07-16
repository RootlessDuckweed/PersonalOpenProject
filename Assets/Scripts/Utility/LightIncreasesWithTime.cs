using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Utility
{
    public class LightIncreasesWithTime : MonoBehaviour
    {
        private Light2D light2D => GetComponent<Light2D>();
        [SerializeField] private float targetIntensity;
        [SerializeField] private float increasedSpeed;

        private void Update()
        {
            light2D.intensity = Mathf.Lerp(light2D.intensity, targetIntensity, increasedSpeed * Time.deltaTime);
            //light2D.pointLightOuterRadius = Mathf.Lerp(light2D.pointLightOuterRadius, targetIntensity*2, increasedSpeed * Time.deltaTime);
        }
    }
}