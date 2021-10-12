using Behaviours.Player;
using UnityEngine;

namespace Test.Builders.Behaviours.Player
{
    public class PlayerHealthBuilder : GameObjectBuilder<PlayerHealth>
    {
        public PlayerHealthBuilder()
        {
        }

        public PlayerHealthBuilder(GameObject gameObject) : base(gameObject)
        {
        }
    }
}