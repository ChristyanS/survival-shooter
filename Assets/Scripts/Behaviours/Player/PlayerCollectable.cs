using System;
using Behaviours.Managers;
using Enums;
using UnityEngine;

namespace Behaviours.Player
{
    public class PlayerCollectable : MonoBehaviour
    {
        [SerializeField] private GameObject handObject;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tag.Loot.ToString()))
            {
                Collect(other);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(Tag.Purchasable.ToString()))
                if (VirtualInputInputManager.Instance.PressActionButton)
                    Collect(other);
        }

        private void OnValidate()
        {
            if (handObject == null)
                throw new ArgumentException("No hand object found");
        }

        private void Collect(Collider other)
        {
            var item = Item.Item.GetItem(other);

            if (ScoreManager.Instance.CanSubtractScore(item.Value))
                ScoreManager.Instance.SubScore(item.Value);

            item.Execute(handObject);

            Destroy(other.gameObject);
        }
    }
}