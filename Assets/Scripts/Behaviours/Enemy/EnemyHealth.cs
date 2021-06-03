using System;
using Behaviours.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] [Range(1, 200)] private int startingHealth = 100;
        [SerializeField] [Range(1, 20)] private float sinkSpeed = 2.5f;
        [SerializeField] [Range(0, 5)] private float sinkTime = 2;
        [SerializeField] [Range(0, 200)] private int scoreValue = 10;
        private CapsuleCollider _capsuleCollider;
        private int _currentHealth;
        private bool _isDead;
        private bool _isSinking;
        private NavMeshAgent _navMeshAgent;
        public bool IsAlive => _currentHealth > 0;


        private void Start()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _currentHealth = startingHealth;
            Validate();
        }

        private void Update()
        {
            if (_isSinking)
            {
                transform.Translate(-Vector3.up * (sinkSpeed * Time.deltaTime));
            }
        }

        private void Validate()
        {
            if (_capsuleCollider == null)
                throw new ArgumentException("No capsule collider is found");
            if (_navMeshAgent == null)
                throw new ArgumentException("No nav mesh agent is found");
        }

        public void TakeDamage(int amount, Vector3 hitPoints)
        {
            if (!_isDead)
            {
                _currentHealth -= amount;
                if (!IsAlive)
                {
                    Death();
                }
            }
        }

        private void Death()
        {
            _isDead = true;
            _capsuleCollider.isTrigger = true;
            StartSinking();
        }

        private void StartSinking()
        {
            _navMeshAgent.enabled = false;
            _isSinking = true;
            ScoreManager.Score += scoreValue;
            Destroy(gameObject, sinkTime);
        }
    }
}