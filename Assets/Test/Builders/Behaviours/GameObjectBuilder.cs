using UnityEngine;

namespace Test.Builders.Behaviours
{
    public class GameObjectBuilder<T> where T : Component
    {
        public GameObjectBuilder()
        {
            GameObject = new GameObject();
        }

        public GameObjectBuilder(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public T Component { get; private set; }

        public GameObject GameObject { get; }

        public GameObjectBuilder<T> Build()
        {
            Component = GameObject.AddComponent<T>();
            return this;
        }

        public GameObjectBuilder<T> WithTag(string tag)
        {
            Component.tag = tag;
            return this;
        }
    }
}