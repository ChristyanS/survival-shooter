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
        [SerializeField] private List<GameObject> items;
        [SerializeField] [Range(1, 100)] private int dropPercentage = 10;

        private void OnValidate()
        {
            if (items.Any(o => !o.CompareTag(Tag.Loot.ToString())))
                throw new ArgumentException("All items must have loot tag");

            if (items.Any(o => o.GetComponent<Collider>() == null))
                throw new ArgumentException("All items must have collider");

            if (items.Any(o => !o.GetComponent<Collider>().isTrigger))
                throw new ArgumentException("All collider must be trigger");
        }

        public void Drop()
        {
            if (CanDropItem())
            {
                var localPosition = transform.position;
                Instantiate(items[Random.Range(0, items.Count)], new Vector3(localPosition.x, 0, localPosition.z),
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