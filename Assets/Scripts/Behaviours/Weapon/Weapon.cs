using System;
using System.Collections;
using Behaviours.Player;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviours.Weapon
{
    public class Weapon : MonoBehaviour
    {
        private const float LightActiveTime = 0.1f;
        private static readonly int IsShooting = Animator.StringToHash("isShooting");
        [SerializeField] private Transform bullet;
        [SerializeField] [Range(0.001f, 2)] private float timeBetweenBullets = 0.15f;
        [SerializeField] [Range(0.001f, 1)] private float recoil = 0.2f;
        [SerializeField] [Range(1, 100)] private float speed = 1;
        [SerializeField] [Range(1, 1000)] private int damage = 10;
        [SerializeField] private GameObject barrel;
        private AudioSource _audioSource;
        private Light _gunLight;
        private ParticleSystem _gunParticles;
        private bool _isShooting;
        private Animator _playerAnimator;
        private PlayerHealth _playerHealth;

        private bool CanShooting => Input.GetButton(Axis.Fire1.ToString()) && !_isShooting && _playerHealth.IsAlive;

        private void Start()
        {
            _gunLight = GetComponent<Light>();
            _audioSource = GetComponent<AudioSource>();
            _gunParticles = GetComponentInChildren<ParticleSystem>();
            var player = GameObject.FindGameObjectWithTag(Tag.Player.ToString());
            _playerHealth = player.GetComponent<PlayerHealth>();
            _playerAnimator = player.GetComponent<Animator>();
            Validate();
        }

        private void Update()
        {
            if (Input.GetButton(Axis.Fire1.ToString()))
                _playerAnimator.SetBool(IsShooting, true);
            else
                _playerAnimator.SetBool(IsShooting, false);

            if (CanShooting)
                StartCoroutine(Shoot());
        }

        private void Validate()
        {
            if (_gunLight == null)
                throw new ArgumentException("No light found");
            if (_audioSource == null)
                throw new ArgumentException("No audio source found");
            if (_gunParticles == null)
                throw new ArgumentException("No particle system is found");
            if (_playerHealth == null)
                throw new ArgumentException("No player health found");
            if (barrel == null)
                throw new ArgumentException("No barrel is found");
        }

        private IEnumerator Shoot()
        {
            _isShooting = true;
            _audioSource.Play();
            _gunParticles.Stop();
            _gunParticles.Play();
            var direction = barrel.transform;
            var bulletInstantiate = Instantiate(bullet, direction.position, direction.rotation);

            var value = Random.Range(-recoil, recoil);

            var signal = Mathf.Sign(value);

            StartCoroutine(ShootLight());

            bulletInstantiate.GetComponent<Bullet>()
                .Setup(Vector3.Slerp(direction.forward, signal * direction.right, Mathf.Abs(value)), speed, damage);

            yield return new WaitForSeconds(timeBetweenBullets);

            _isShooting = false;
        }

        private IEnumerator ShootLight()
        {
            _gunLight.enabled = true;
            yield return new WaitForSeconds(LightActiveTime);
            _gunLight.enabled = false;
        }
    }
}