using System;
using System.Collections;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviours.Gun
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform bullet;
        [SerializeField] [Range(0.001f, 1)] private float timeBetweenBullets = 0.15f;
        [SerializeField] [Range(0.001f, 1)] private float recoil = 0.2f;
        [SerializeField] [Range(1, 100)] private float speed = 1;
        [SerializeField] [Range(1, 1000)] private int damage = 10;

        private Light _gunLight;
        private bool _isShooting;
        private bool CanShooting => Input.GetButton(Axis.Fire1.ToString()) && !_isShooting;

        private void Start()
        {
            _gunLight = GetComponent<Light>();
            Validate();
        }

        private void Update()
        {
            if (CanShooting)
            {
                StartCoroutine(Shoot());
            }
        }

        private void Validate()
        {
            if (_gunLight == null)
                throw new ArgumentException("No light found");
        }

        private IEnumerator Shoot()
        {
            _gunLight.enabled = true;
            _isShooting = true;

            var direction = transform;
            var bulletInstantiate = Instantiate(bullet, direction.position, direction.rotation);

            var value = Random.Range(-recoil, recoil);

            var signal = value >= 0 ? 1 : -1;

            bulletInstantiate.GetComponent<Bullet>()
                .Setup(Vector3.Slerp(direction.forward, signal * direction.right, Mathf.Abs(value)), speed, damage);

            yield return new WaitForSeconds(timeBetweenBullets);

            _isShooting = false;
            _gunLight.enabled = false;
        }
    }
}