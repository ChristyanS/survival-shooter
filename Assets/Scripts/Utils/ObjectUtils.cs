using UnityEngine;

namespace Utils
{
    public static class ObjectUtils
    {
        public static void DestroyAll<T>() where T : Object
        {
            var gameObjects = Object.FindObjectsOfType<T>();
            foreach (var gameObject in gameObjects) Object.Destroy(gameObject);
        }
    }
}