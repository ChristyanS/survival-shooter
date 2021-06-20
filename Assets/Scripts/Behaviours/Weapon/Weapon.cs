using System;
using System.Collections;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviours.Gun
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform bullet;
        [SerializeField] [Range(0.001f, 2)] private float timeBetweenBullets = 0.15f;
        [SerializeField] [Range(0.001f, 1)] private float recoil = 0.2f;
        [SerializeField] [Range(1, 100)] private float speed = 1;
        [SerializeField] [Range(1, 1000)] private int damage = 10;
        private AudioSource _audioSource;
        private Light _gunLight;
        private ParticleSystem _gunParticles;
        private bool _isShooting;
        private bool CanShooting => Input.GetButton(Axis.Fire1.ToString()) && !_isShooting;

        private void Start()
        {
            _gunLight = GetComponent<Light>();
            _audioSource = GetComponent<AudioSource>();
            _gunParticles = GetComponent<ParticleSystem>();
            Validate();
        }

        private void Update()
        {
            if (CanShooting) StartCoroutine(Shoot());
        }

        private void Validate()
        {
            if (_gunLight == null)
                throw new ArgumentException("No light found");
            if (_audioSource == null)
                throw new ArgumentException("No audio source found");
            if (_gunParticles == null)
                throw new ArgumentException("No particle system is found");
        }

        private IEnumerator Shoot()
        {
            _gunLight.enabled = true;
            _isShooting = true;
            _audioSource.Play();
            _gunParticles.Stop();
            _gunParticles.Play();
            var direction = transform;
            var bulletInstantiate = Instantiate(bullet, direction.position, direction.rotation);

            var value = Random.Range(-recoil, recoil);

            var signal = Mathf.Sign(value);

            bulletInstantiate.GetComponent<Bullet>()
                .Setup(Vector3.Slerp(direction.forward, signal * direction.right, Mathf.Abs(value)), speed, damage);

            yield return new WaitForSeconds(timeBetweenBullets);

            _isShooting = false;
            _gunLight.enabled = false;
        }
    }
}