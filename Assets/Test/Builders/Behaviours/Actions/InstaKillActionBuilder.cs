using Behaviours.Actions;
using UnityEngine;

namespace Test.Builders.Behaviours.Actions
{
    public class InstaKillActionBuilder : GameObjectBuilder<InstaKillAction>
    {
        public InstaKillActionBuilder()
        {
        }

        public InstaKillActionBuilder(GameObject gameObject) : base(gameObject)
        {
        }

        public InstaKillActionBuilder(string name) : base(name)
        {
        }

        public InstaKillActionBuilder WithPowerUpImage()
        {
            Component.PowerUpImage = new GameObject("PowerUpImage");
            return this;
        }

        public InstaKillActionBuilder WithTime(float time)
        {
            Component.Time = time;
            return this;
        }
    }
}