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

        public List<GameObject> Items
        {
            get => items;
            set => value = items;
        }

        private void OnValidate()
        {
            if (items == null)
                throw new ArgumentException("Item list not found, please enter an item list");

            if (items.First() == null)
                throw new ArgumentException(
                    "Item list does not contain an element, please add a GameObject to the list ");

            if (items.Any(o => !o.CompareTag(Tag.Loot.ToString())))
                throw new ArgumentException(
                    "Any item without the Tag Loot was found. Please insert the Loot Tag to GameObject");

            if (items.Any(o => o.GetComponent<Collider>() == null))
                throw new ArgumentException("Any item without collider found, please insert collider to Object ");

            if (items.Any(o => !o.GetComponent<Collider>().isTrigger))
                throw new ArgumentException("Any non-trigger collider found, please add trigger to all colliders ");
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