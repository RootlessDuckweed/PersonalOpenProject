using System;
using System.Collections;
using Cinemachine;
using Player.Universal;
using TMPro;
using UI.InGameUI.HealthBarUI;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

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

        [Header("Ailment particles")] 
        [SerializeField] private ParticleSystem igniteFX;
        [SerializeField] private ParticleSystem chillFX;
        [SerializeField] private ParticleSystem shockFX;

        [Header("CriticalHit FX")] 
        [SerializeField] private GameObject criticalHit;
        
        [Header("Running FX")]
        [SerializeField] private ParticleSystem runDust;

        [Header("Dash Shadow FX")] 
        [SerializeField] private GameObject afterImagePrefab;
        [SerializeField] private float colorLooseRate;
        private ObjectPool<GameObject> shadowPool;

        [Header("Screen Shake FX")] 
        [SerializeField] private CinemachineImpulseSource screenShake;
        [SerializeField] private float shakeMultiplier;
        [SerializeField] private Vector3 shakePower;

        [Header("Hide Health bar")] 
        [SerializeField] private GameObject healthBar;
        private void Awake()
        {
            shadowPool = new ObjectPool<GameObject>(CreateShadow, ShadowOnGet, ShadowOnRelease, ShadowOnDestroy);
            screenShake = GetComponent<CinemachineImpulseSource>();
            healthBar = GetComponentInChildren<HealthBar>()?.gameObject;
        }
        
        private void Start()
        {
            _sr = GetComponentInChildren<SpriteRenderer>();
            _originalMat = _sr.material;
        }

       private IEnumerator  FlashHitFX()
       {
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
           CancelInvoke("RedColorBlink");
       }

       private void CancelIgniteColorBlink()
       {
           CancelInvoke("IgniteColorFx");
           _sr.color = Color.white;
          igniteFX.Stop();
       }
       private void CancelChillColorBlink()
       {
           CancelInvoke("ChillColorFx");
           _sr.color = Color.white;
          chillFX.Stop();
       }
       private void CancelShockColorBlink()
       {
           CancelInvoke("ShockColorFx");
           _sr.color = Color.white;
          shockFX.Stop();
       }

       
       public void IgniteFXFor(float seconds)
       {
           igniteFX.Play();
           InvokeRepeating("IgniteColorFx",0,.3f);
           Invoke("CancelIgniteColorBlink",seconds);
       }

       public void ChillFXFor(float seconds)
       {
           chillFX.Play();
           InvokeRepeating("ChillColorFx",0,.3f);
           Invoke("CancelChillColorBlink",seconds);
       }
       public void ShockFXFor(float seconds)
       {
           shockFX.Play();
           InvokeRepeating("ShockColorFx",0,.3f);
           Invoke("CancelShockColorBlink",seconds);
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

       public void GenerateCriticalHitFX(GameObject attacker,GameObject target)
       {
           var hit = Instantiate(criticalHit,target.transform.position-new Vector3(0.2f,0,0),Quaternion.identity);
           float zRotation = Random.Range(-80, 80);
           hit.transform.Rotate(Vector3.forward,zRotation);
           if (target.transform.position.x < attacker.transform.position.x)
           {
               hit.transform.localScale = new Vector3(-hit.transform.localScale.x, hit.transform.localScale.y, hit.transform.localScale.z);
           }
           Destroy(hit,0.5f);
       }

       public void PlayRunningDust()
       {
           if (runDust != null)
           {
               runDust.Play();
           }
       }

       public void ScreenShake()
       {
           screenShake.m_DefaultVelocity =
               new Vector3(shakePower.x * PlayerManager.Instance.Player.facingDir, shakePower.y) * shakeMultiplier;
           screenShake.GenerateImpulse();
       }

      

       #region ShadowPool
       
       private void ShadowOnDestroy(GameObject obj)
       {
           Destroy(obj);
       }

       private void ShadowOnRelease(GameObject obj)
       {
          obj.SetActive(false);
       }

       private void ShadowOnGet(GameObject obj)
       {
          obj.SetActive(true);
       }

       private GameObject CreateShadow()
       {
           var shadow = Instantiate(afterImagePrefab);
           shadow.GetComponent<AfterImageFX>().SetupAfterImage(colorLooseRate,GetComponent<Entity>());
           shadow.SetActive(false);
           return shadow;
       }

       public void ReturnToShadowPool(GameObject obj)
       {
           shadowPool.Release(obj);
       }

       public GameObject GetOnShadowPool()
       {
           return shadowPool.Get();
       }
       
       #endregion

       public void MakeTransparent(bool transparent)
       {
           if (transparent)
           {
               healthBar.SetActive(false);
               _sr.color = Color.clear;
           }
           else
           {
               healthBar.SetActive(true);
               _sr.color = Color.white;
           }
       }
    }
}