using System;
using System.Collections;
using Behaviours.Player;
using Enums;
using UnityEngine;

namespace Behaviours.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] [Range(0.1f, 10)] private float timeBetweenAttacks = 0.5f;
        [SerializeField] [Range(1, 200)] private int attackDamage = 10;
        private bool _isAttacking;
        private bool _isPlayerInRange;

        private GameObject _player;
        private PlayerHealth _playerHealth;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag(Tag.Player.ToString());
            _playerHealth = _player.GetComponent<PlayerHealth>();

            Validate();
        }

        private void Update()
        {
            if (_isPlayerInRange && !_isAttacking)
                StartCoroutine(Attack());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _player)
            {
                _isPlayerInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _player)
            {
                _isPlayerInRange = false;
            }
        }

        private void Validate()
        {
            if (_player == null)
                throw new ArgumentException("No player game object is found");

            if (_playerHealth == null)
                throw new ArgumentException("No player health found");

            var sphereCollider = GetComponent<SphereCollider>();

            if (sphereCollider == null)
                throw new ArgumentException("No sphere collider found");

            if (!sphereCollider.isTrigger)
                throw new ArgumentException("Sphere collider is not a trigger");
        }

        private IEnumerator Attack()
        {
            if (_playerHealth.IsAlive)
            {
                _playerHealth.TakeDamage(attackDamage);
                _isAttacking = true;

                yield return new WaitForSeconds(timeBetweenAttacks);

                _isAttacking = false;
            }
        }
    }
}