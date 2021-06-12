using Behaviours.Enemy;
using Enums;
using UnityEngine;

namespace Behaviours.Actions
{
    public class Nuke : MonoBehaviour
    {
        public void Action()
        {
            var enemies = GameObject.FindGameObjectsWithTag(Tag.Enemy.ToString());
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(1000); //todo ver uma forma de dar o máximo de dano
            }
        }
    }
}