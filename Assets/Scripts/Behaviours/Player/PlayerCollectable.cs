using System;
using System.Diagnostics;
using Behaviours.Actions;
using Behaviours.Item;
using Behaviours.Utils;
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
                var item = Item.Item.GetItem(other);

                switch (item.ItemType)
                {
                    case ItemType.Action:
                        var nuke = GetNuke(item.Loot);
                        nuke.Action();
                        break;
                    case ItemType.Collectable:
                        BehaviourUtils.DestroyAllChilds(handObject.transform);
                        var weapon = InstantiateWeapon(item.Loot);
                        AddWeaponToHand(weapon);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(item.ItemType));
                }

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

        private Nuke GetNuke(GameObject other)
        {
            var nuke = other.gameObject.GetComponent<Nuke>();

            if (nuke == null)
                throw new ArgumentException("No nuke object found");

            return nuke;
        }
    }
}