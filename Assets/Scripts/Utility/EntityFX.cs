using System;
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
    }
}