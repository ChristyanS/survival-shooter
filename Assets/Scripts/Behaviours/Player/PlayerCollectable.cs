using System;
using Behaviours.Item;
using Enums;
using UnityEngine;

namespace Behaviours.Player
{
    public class PlayerCollectable : MonoBehaviour
    {
        [SerializeField] private GameObject handObject;
        
        private void OnValidate()
        {
            if (handObject == null)
                throw new ArgumentException("No hand object found");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tag.Loot.ToString()))
            {
                DestroyAllChilds(handObject.transform);

                var item = GetWeaponItem(other);

                var weapon = InstantiateWeapon(item);

                AddWeaponToHand(weapon);

                Destroy(other.gameObject);
            }
        }

        private void AddWeaponToHand(GameObject weapon)
        {
            weapon.transform.parent = handObject.transform;
        }

        private GameObject InstantiateWeapon(GameObject item)
        {
            var direction = handObject.transform;

            var weapon = Instantiate(item, direction.position,
                direction.rotation);
            return weapon;
        }

        private GameObject GetWeaponItem(Collider collider)
        {
            var item = collider.gameObject.GetComponent<WeaponItem>();

            if (item == null)
                throw new ArgumentException("No item object found");

            return item.Item;
        }

        private void DestroyAllChilds(Transform transformLocal)
        {
            foreach (Transform child in transformLocal)
            {
                Destroy(child.gameObject);
            }
        }
    }
}