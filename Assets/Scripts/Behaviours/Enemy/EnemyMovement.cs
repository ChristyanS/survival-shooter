using System;
using Behaviours.Player;
using Enums;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private EnemyHealth _enemyHealth;
        private NavMeshAgent _navMeshAgent;
        private PlayerHealth _playerHealth;
        private Transform _playerTransform;

        public void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag(Tag.Player.ToString()).transform;
            _playerHealth = _playerTransform.GetComponent<PlayerHealth>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyHealth = GetComponent<EnemyHealth>();

            Validate();
        }

        private void Update()
        {
            if (_enemyHealth.IsAlive && _playerHealth.IsAlive)
            {
                if (_navMeshAgent.isOnNavMesh)
                    _navMeshAgent.SetDestination(_playerTransform.position);
            }
            else
            {
                _navMeshAgent.enabled = false;
            }
        }

        private void Validate()
        {
            if (_navMeshAgent == null)
                throw new ArgumentException("No navmesh agent found");
            if (!_navMeshAgent.isOnNavMesh)
                throw new ArgumentException("No navmesh configuration");
            if (_playerHealth == null)
                throw new ArgumentException("No player health found");
            if (_enemyHealth == null)
                throw new ArgumentException("No enemy health found");
        }
    }
}