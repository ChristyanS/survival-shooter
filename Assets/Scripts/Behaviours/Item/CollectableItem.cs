using System;
using Enums;
using UnityEngine;

namespace Behaviours.Item
{
    public class CollectableItem : Item
    {
        private GameObject _handObject;
        public override ItemType ItemType => ItemType.Collectable;

        public override void Execute(GameObject other = null)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            _handObject = other;
            var weapon = ActiveWeapon(Loot);
        }


        private GameObject ActiveWeapon(GameObject item)
        {
            for (var i = 0; i < _handObject.transform.childCount; i++)
            {
                var o = _handObject.transform.GetChild(i);
                if (o.CompareTag(Tag.Weapon.ToString())) o.gameObject.SetActive(item.name == o.name);
            }

            return null;
        }
    }
}