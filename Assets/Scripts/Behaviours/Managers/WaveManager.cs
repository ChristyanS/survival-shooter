using UnityEngine;
using UnityEngine.UI;

namespace Behaviours.Managers
{
    public class WaveManager : MonoBehaviour
    {
        public Text waveCount;
        private static int _waveNumber;
        private static int _enemiesNumber;
        public static int EnemiesSpawned;
        public static int DeadEnemies;
        public static bool AllEnemiesDie => DeadEnemies >= _enemiesNumber;
        public static bool SpawnedAllEnemies => EnemiesSpawned <= _enemiesNumber;

        private void Start()
        {
            _waveNumber = 1;
            _enemiesNumber = 20;
            EnemiesSpawned = 0;
            DeadEnemies = 0;
        }

        private void Update()
        {
            waveCount.text = _waveNumber.ToString();
        }

        public static void NextWave()
        {
            _waveNumber++;
            _enemiesNumber += 2;
            EnemiesSpawned = 0;
            DeadEnemies = 0;
        }
    }
}