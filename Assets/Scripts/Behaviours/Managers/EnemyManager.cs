using System;
using System.Linq;
using Behaviours.Player;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviours.Managers
{
    [RequireComponent(typeof(GameObject))]
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] [Range(1, 100)] private float spawnTime = 3f;
        private PlayerHealth _playerHealth;


        private void Start()
        {
            _playerHealth = GameObject.FindGameObjectWithTag(Tag.Player.ToString()).GetComponent<PlayerHealth>();

            Validate();

            InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
        }

        private void Validate()
        {
            if (_playerHealth == null)
                throw new ArgumentException("No player health found");


            if (enemy == null)
                throw new ArgumentException("No enemy found");

            if (!spawnPoints.Any())
                throw new ArgumentException("No spawn point setup");
        }


        private void Spawn()
        {
            if (_playerHealth.IsAlive)
            {
                WaveManager.Instance.AddEnemySpawned();
                if (WaveManager.Instance.SpawnedAllEnemies)
                {
                    var spawnPointIndex = Random.Range(0, spawnPoints.Length);
                    Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                }
            }
        }
    }
}