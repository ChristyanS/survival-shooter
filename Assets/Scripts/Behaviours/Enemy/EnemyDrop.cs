using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviours.Enemy
{
    public class EnemyDrop : MonoBehaviour
    {
        [SerializeField] private List<GameObject> weapons;
        [SerializeField] [Range(1, 100)] private int dropPercentage = 10;

        private void OnValidate()
        {
            if (weapons.Any(o => !o.CompareTag(Tag.Loot.ToString())))
                throw new ArgumentException("All weapons must have loot tag");

            if (weapons.Any(o => o.GetComponent<Collider>() == null))
                throw new ArgumentException("All weapons must have collider");
            
            if (weapons.Any(o => !o.GetComponent<Collider>().isTrigger))
                throw new ArgumentException("All collider must be trigger");
        }

        public void Drop()
        {
            if (CanDropItem())
            {
                Instantiate(weapons[Random.Range(0, weapons.Count - 1)], transform.position,
                    Quaternion.identity);
            }
        }

        private bool CanDropItem()
        {
            var randomNumber = Random.Range(1, 101);
            return randomNumber <= dropPercentage;
        }
    }
}