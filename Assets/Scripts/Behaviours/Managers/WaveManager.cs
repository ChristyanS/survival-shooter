using System;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours.Managers
{
    public class WaveManager : Singleton<WaveManager>
    {
        [SerializeField] private Text waveCountText;
        [SerializeField] [Range(1, 100)] private int waveNumber = 1;
        [SerializeField] [Range(1, 1000)] private int enemiesNumber = 20;
        [SerializeField] [Range(1, 100)] private int enemiesNumberIncrease = 2;
        private int _deadEnemies;
        private int _enemiesSpawned;
        public bool AllEnemiesDie => _deadEnemies >= enemiesNumber;
        public bool SpawnedAllEnemies => _enemiesSpawned <= enemiesNumber;

        private void Start()
        {
            if (waveCountText == null)
                throw new ArgumentException("No text count found");
        }

        private void Update()
        {
            waveCountText.text = waveNumber.ToString();
        }

        private void NextWave()
        {
            waveNumber++;
            enemiesNumber += enemiesNumberIncrease;
            _enemiesSpawned = 0;
            _deadEnemies = 0;
        }

        public void AddDeadEnemies()
        {
            _deadEnemies++;
            if (AllEnemiesDie)
                NextWave();
        }

        public void AddEnemySpawned()
        {
            _enemiesSpawned++;
        }
    }
}