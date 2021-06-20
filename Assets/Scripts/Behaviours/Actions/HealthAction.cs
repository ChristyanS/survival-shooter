using Behaviours.Player;
using Enums;
using UnityEngine;

namespace Behaviours.Actions
{
    public class HealthAction : ActionBehaviour
    {
        [SerializeField] [Range(1, 100)] private int health = 10;

        public override void Execute()
        {
            var player = GameObject.FindGameObjectWithTag(Tag.Player.ToString());
            player.GetComponent<PlayerHealth>().AddHealth(health);
        }
    }
}