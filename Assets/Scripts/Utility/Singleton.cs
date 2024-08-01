using UnityEngine;

namespace Utility
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        //泛型单例模式
        private static T instance;

        public static T Instance
        {
            get { return instance; }
        }

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = (T)this;
            }
        }
    }
}