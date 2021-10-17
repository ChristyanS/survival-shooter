using Behaviours.Actions;
using UnityEngine;

namespace Test.Builders.Behaviours.Actions
{
    public class DoublePointsActionBuilder : GameObjectBuilder<DoublePointsAction>
    {
        public DoublePointsActionBuilder()
        {
        }

        public DoublePointsActionBuilder(GameObject gameObject) : base(gameObject)
        {
        }

        public DoublePointsActionBuilder(string name) : base(name)
        {
        }

        public DoublePointsActionBuilder WithPowerUpImage()
        {
            Component.PowerUpImage = new GameObject("PowerUpImage");
            return this;
        }

        public DoublePointsActionBuilder WithTime(float time)
        {
            Component.Time = time;
            return this;
        }
    }
}