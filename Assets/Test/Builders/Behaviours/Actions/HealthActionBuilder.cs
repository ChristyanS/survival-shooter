using Behaviours.Actions;
using UnityEngine;

namespace Test.Builders.Behaviours.Actions
{
    public class HealthActionBuilder : GameObjectBuilder<HealthAction>
    {
        public HealthActionBuilder()
        {
        }

        public HealthActionBuilder(GameObject gameObject) : base(gameObject)
        {
        }

        public HealthActionBuilder(string name) : base(name)
        {
        }
    }
}