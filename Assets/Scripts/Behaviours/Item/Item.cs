using System;
using System.Collections;
using Enums;
using UnityEngine;

namespace Behaviours.Item
{
    public abstract class Item : MonoBehaviour
    {
        private const float TimeToDestroy = 40;
        [SerializeField] private GameObject loot;

        protected GameObject Loot => loot;
        public abstract ItemType ItemType { get; }

        private void Start()
        {
            StartCoroutine(Destroy());
        }

        private void OnValidate()
        {
            if (loot == null)
                throw new ArgumentException("No item object found");

            if (!gameObject.CompareTag(Tag.Loot.ToString()))
                throw new ArgumentException("All item must have loot tag");
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(TimeToDestroy);
            Destroy(gameObject);
        }

        public abstract void Execute(GameObject other = null);

        public static Item GetItem(Collider collider)
        {
            var item = collider.gameObject.GetComponent<Item>();

            if (item == null)
                throw new ArgumentException("No item object found");

            return item;
        }
    }
}