using System;
using System.Diagnostics;
using Behaviours.Gun;
using Enums;
using UnityEngine;

namespace Behaviours.Item
{
    //todo transformar em abstrato
    public class Item : MonoBehaviour 
    {
        [SerializeField] private GameObject loot;
        [SerializeField] private Tag lootTag;
        [SerializeField] private ItemType itemType;

        public GameObject Loot => loot;
        public ItemType ItemType => itemType;

        private void OnValidate()
        {
            switch (itemType)
            {
                case ItemType.Collectable:
                    if (loot == null)
                        throw new ArgumentException("No item object found");

                    if (!loot.CompareTag(lootTag.ToString()))
                        throw new ArgumentException($"All item must have {lootTag} tag");

                    break;
                case ItemType.Action:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (!gameObject.CompareTag(Tag.Loot.ToString()))
                throw new ArgumentException($"All item must have loot tag");
        }

        public static Item GetItem(Collider collider)
        {
            var item = collider.gameObject.GetComponent<Item>();

            if (item == null)
                throw new ArgumentException("No item object found");

            return item;
        }
    }
}