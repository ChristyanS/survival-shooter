using UnityEngine;

namespace Behaviours.Managers
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<T>();
                if (_instance == null)
                    Debug.LogError("Singleton<" + typeof(T) + "> instance has been not found.");
                return _instance;
            }
        }

        protected void Awake()
        {
            if (GetType() != typeof(T))
                DestroySelf();
            if (_instance == null)
                _instance = this as T;
            else if (_instance != this)
                DestroySelf();
            DontDestroyOnLoad(gameObject);
        }

        private void DestroySelf()
        {
            if (Application.isPlaying)
                Destroy(this);
            else
                DestroyImmediate(this);
        }
    }
}