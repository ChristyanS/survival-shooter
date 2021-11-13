using System;
using Behaviours.Managers;
using Behaviours.Purchasable;
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

            if (other.CompareTag(Tag.UnLockable.ToString()))
                if (VirtualInputInputManager.Instance.PressActionButton)
                    Unlock(other);
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
            var mysteryBox = MysteryBox.GetMysteryBox(other);

            MoneyManager.Instance.SubMoney(mysteryBox.Value);

            mysteryBox.Open();
        }

        private void Unlock(Collider other)
        {
            var unlockable = Unlockable.GetUnlockable(other);

            MoneyManager.Instance.SubMoney(unlockable.Value);

            unlockable.Unlock();
        }
    }
}