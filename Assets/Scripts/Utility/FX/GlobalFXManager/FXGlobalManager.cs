using System;
using Player.Universal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Utility.FX.GlobalFXManager
{
    public class FXGlobalManager : Singleton<FXGlobalManager>
    {
        [Header("Pop UP Text")] 
        [SerializeField] private GameObject popUpTextPrefab;
        private ObjectPool<GameObject> popTextPool;
        

        protected override void Awake()
        {
            base.Awake();
            popTextPool = new ObjectPool<GameObject>(CreatePop, PopTextOnGet, PopTextOnRelease, PopTextOnDestroy);
        }
        public void CreatePopUpText(string text,Transform target)
        {
            float randomX = Random.Range(-1, 1);
            float randomY = Random.Range(1, 3);
            Vector3 offset = new Vector3(randomX, randomY,0);
            var newText = GetOnPopTextPool();
            newText.transform.position  = target.position + offset;
            newText.GetComponent<PopUpTextFX>().SetupPopText(text);
        }
        
        #region PopTextPool
       
        private void PopTextOnDestroy(GameObject obj)
        {
            Destroy(obj);
        }

        private void PopTextOnRelease(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void PopTextOnGet(GameObject obj)
        {
            obj.SetActive(true);
        }

        private GameObject CreatePop()
        {
            var newText = Instantiate(popUpTextPrefab,transform);
            return newText;
        }
       
        public GameObject GetOnPopTextPool()
        {
            return popTextPool.Get();
        }
       
        public void ReturnToTextPool(GameObject obj)
        {
            popTextPool.Release(obj);
        }
        #endregion
    }
}
