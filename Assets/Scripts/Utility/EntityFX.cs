using System.Collections;
using UnityEngine;

namespace Utility
{
    public class EntityFX : MonoBehaviour
    {
        [Header("AttackFX Info")]
        private SpriteRenderer _sr;
        [SerializeField] private Material _hitMat;
        [SerializeField] private Material _originalMat;
        [SerializeField] private float _flashHitFX;

        [Header("Ailments Colors")] 
        [SerializeField] private Color[] igniteColor;
        [SerializeField] private Color[] chillColor;
        [SerializeField] private Color[] shockColor;
        private void Start()
        {
            _sr = GetComponentInChildren<SpriteRenderer>();
            _originalMat = _sr.material;
        }

       private IEnumerator  FlashHitFX()
       {
           print("FlashHitFX");
           _sr.material = _hitMat;
           yield return new WaitForSeconds(_flashHitFX);
           _sr.material = _originalMat;
       }

       private void RedColorBlink()
       {
           if (_sr.color != Color.white)
           {
               _sr.color = Color.white;
           }
           else
           {
               _sr.color = Color.red;
           }
          
       }

       private void CancelColorBlink()
       {
           CancelInvoke();
           _sr.color = Color.white;
          
       }

       
       public void IgniteFXFor(float seconds)
       {
           InvokeRepeating("IgniteColorFx",0,.3f);
           Invoke("CancelColorBlink",seconds);
       }

       public void ChillFXFor(float seconds)
       {
           InvokeRepeating("ChillColorFx",0,.3f);
           Invoke("CancelColorBlink",seconds);
       }
       public void ShockFXFor(float seconds)
       {
           InvokeRepeating("ShockColorFx",0,.3f);
           Invoke("CancelColorBlink",seconds);
       }

       private void ChillColorFx()
       {
           _sr.color = _sr.color != chillColor[0] ? chillColor[0] : chillColor[1];
       }

       
       private void ShockColorFx()
       {
           _sr.color = _sr.color != shockColor[0] ? shockColor[0] : shockColor[1];
       }
       
       private void IgniteColorFx()
       {
           _sr.color = _sr.color != igniteColor[0] ? igniteColor[0] : igniteColor[1];
       }
       
    }
}