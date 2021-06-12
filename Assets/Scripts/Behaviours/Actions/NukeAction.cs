using Behaviours.Enemy;
using Enums;
using UnityEngine;

namespace Behaviours.Actions
{
    public class NukeAction : ActionBehaviour
    {
        public override void Execute()
        {
            var enemies = GameObject.FindGameObjectsWithTag(Tag.Enemy.ToString());
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage();
            }
        }
    }
}