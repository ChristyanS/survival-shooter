using System;
using Enums;
using UnityEngine;

namespace Behaviours.Item
{
    public class WeaponItem : MonoBehaviour
    {
        [SerializeField] private GameObject item;

        public GameObject Item => item;

        private void OnValidate()
        {
            if (item == null)
                throw new ArgumentException("No item object found");

            if (!item.CompareTag(Tag.Weapon.ToString()))
                throw new ArgumentException("All item must have weapon tag");
        }
    }
}