using System;
using UnityEngine;

namespace Behaviours.Purchasable
{
    public class Unlockable : MonoBehaviour
    {
        private static readonly int Active = Animator.StringToHash("active");
        [SerializeField] [Range(0, 10000)] private int value;
        private Animator _animator;

        public int Value => value;

        public void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Unlock()
        {
            _animator.SetBool(Active, true);
        }

        public static Unlockable GetUnlockable(Collider collider)
        {
            var item = collider.gameObject.GetComponent<Unlockable>();

            if (item == null)
                throw new ArgumentException("No unlockable object found");

            return item;
        }
    }
}