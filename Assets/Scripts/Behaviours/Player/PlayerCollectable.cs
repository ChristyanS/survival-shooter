using System;
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
                item.Execute(handObject);

                Destroy(other.gameObject);
            }
        }
    }
}