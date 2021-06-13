using System;
using Behaviours.Utils;
using Enums;
using UnityEngine;

namespace Behaviours.Item
{
    public class CollectableItem : Item
    {
        public GameObject handObject;
        public override ItemType ItemType => ItemType.Collectable;

        public override void Execute(GameObject other = null)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            handObject = other;
            BehaviourUtils.DestroyAllChilds(other.transform);
            var weapon = InstantiateWeapon(Loot);
            AddWeaponToHand(weapon);
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
    }
}