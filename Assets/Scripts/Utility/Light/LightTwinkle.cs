using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

namespace Utility
{
    public class LightTwinkle : MonoBehaviour
    {
        private Light2D light2D => GetComponent<Light2D>();
        private float originalIntensity;
        private float timer=0;
      

        private void Start()
        {
            originalIntensity = light2D.intensity;
        }

        private void FixedUpdate()
        {
            timer += Time.deltaTime;
            if (timer > 0.2f)
            {
                timer = 0;
                light2D.intensity = Random.Range(originalIntensity-10f, originalIntensity + 2f);
            }
            
        }
    }
}