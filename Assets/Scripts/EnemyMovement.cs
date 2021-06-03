using System;
using Enums;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Transform _playerTransform;

    public void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag(Tag.Player.ToString()).transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        Validate();
    }

    private void Update()
    {
        if (_navMeshAgent.isOnNavMesh)
            _navMeshAgent.SetDestination(_playerTransform.position);
    }

    private void Validate()
    {
        if (_navMeshAgent == null)
            throw new ArgumentException("No navmesh agent found");
        if (!_navMeshAgent.isOnNavMesh)
            throw new ArgumentException("No navmesh configuration");
    }
}