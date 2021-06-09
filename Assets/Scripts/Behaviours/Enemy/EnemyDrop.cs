using System.Collections.Generic;
using UnityEngine;

namespace Behaviours.Enemy
{
    public class EnemyDrop : MonoBehaviour
    {
        [SerializeField] private List<GameObject> weapons;

        public void Drop()
        {
            Instantiate(weapons[Random.Range(0, weapons.Count - 1)], transform.position,
                Quaternion.identity);
        }
    }
}