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

            if (other.CompareTag(Tag.MysteryBox.ToString()))
                if (VirtualInputInputManager.Instance.PressActionButton)
                    ActiveMysteryBox(other);
        }

        private void OnValidate()
        {
            if (handObject == null)
                throw new ArgumentException("No hand object found");
        }

        private void Collect(Collider other)
        {
            var item = Item.Item.GetItem(other);

            MoneyManager.Instance.SubMoney(item.Value);

            item.Execute(handObject);

            Destroy(other.gameObject);
        }

        private void ActiveMysteryBox(Collider other)
        {
            var mysteryBox = MysteryBox.MysteryBox.GetMysteryBox(other);

            MoneyManager.Instance.SubMoney(mysteryBox.Value);

            mysteryBox.Open();
        }
    }
}