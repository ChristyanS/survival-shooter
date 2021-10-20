using System;
using System.Collections;
using Behaviours.Player;
using Enums;
using UnityEngine;

namespace Behaviours.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int IsAttack = Animator.StringToHash("Attack");
        [SerializeField] [Range(0.1f, 10)] private float timeBetweenAttacks = 0.5f;
        [SerializeField] [Range(1, 200)] private int attackDamage = 10;
        [SerializeField] [Range(0.5f, 5)] private float attackDistance = 2f;
        private Animator _animator;
        private EnemyHealth _enemyHealth;
        private bool _isAttacking;

        private GameObject _player;
        private PlayerHealth _playerHealth;

        private bool IsPlayerInRange =>
            Vector3.Distance(transform.position, _player.transform.position) < attackDistance;

        private bool CanAttack => IsPlayerInRange && !_isAttacking && _enemyHealth.IsAlive;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag(Tag.Player.ToString());
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _enemyHealth = GetComponent<EnemyHealth>();
            _animator = GetComponent<Animator>();
            Validate();
        }

        private void Update()
        {
            if (CanAttack)
                StartCoroutine(StartAttack());
            if (!_playerHealth.IsAlive)
                _animator.SetTrigger(Idle);
        }

        private void Validate()
        {
            if (_player == null)
                throw new ArgumentException("No player game object is found");

            if (_playerHealth == null)
                throw new ArgumentException("No player health found");

            if (_enemyHealth == null)
                throw new ArgumentException("No enemy health found");
        }

        private IEnumerator StartAttack()
        {
            _isAttacking = true;
            _animator.SetBool(IsAttack, true);
            yield return new WaitForSeconds(timeBetweenAttacks);
            _animator.SetBool(IsAttack, false);
            _isAttacking = false;
        }

        // used in animation events
        private void Attack()
        {
            if (_playerHealth.IsAlive && IsPlayerInRange)
                _playerHealth.TakeDamage(attackDamage);
        }
    }
}