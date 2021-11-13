using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviours.Purchasable
{
    public class MysteryBox : MonoBehaviour
    {
        private static readonly int Active = Animator.StringToHash("active");
        [SerializeField] [Range(0, 10000)] private int value;
        [SerializeField] private List<GameObject> items;
        [SerializeField] private GameObject spawn;
        private Animator _animator;

        public int Value => value;

        public void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Open()
        {
            _animator.SetBool(Active, true);
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            yield return new WaitForSeconds(2f);
            var position = spawn.transform.position;
            Instantiate(items[Random.Range(0, items.Count)],
                new Vector3(position.x, 0, position.z), Quaternion.identity);
            _animator.SetBool(Active, false);
        }

        public static MysteryBox GetMysteryBox(Collider collider)
        {
            var item = collider.gameObject.GetComponent<MysteryBox>();

            if (item == null)
                throw new ArgumentException("No mystery box object found");

            return item;
        }
    }
}