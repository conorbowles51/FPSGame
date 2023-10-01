using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager Instance;

        public bool isWaveActive = false;
        public int currentWaveIndex = 0;

        public int enemiesLeftToSpawn;
        public int enemiesAlive;


        [Header("Base Wave Stats")]
        [SerializeField] private int numEnemies;
        [SerializeField] private float enemyDamage;
        [SerializeField] private float enemySpeed;
        [SerializeField] private float timeBetweenSpawns;

        private EnemySpawner enemySpawner;

        private float spawnTimer = 0;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            enemySpawner = EnemySpawner.Instance;
        }

        private void Update()
        {
            if(isWaveActive)
            {
                HandleWave();
            }
        }

        public void BeginNextWave()
        {
            currentWaveIndex++;
            spawnTimer = 0;

            UpgradeWaveStats(5, 2, 1);
            PlayerHUD.Instance.UpdateWaveInfo(currentWaveIndex);

            enemiesLeftToSpawn = numEnemies;
            enemiesAlive = 0;

            isWaveActive = true;
        }

        public void RecordEnemyDeath()
        {
            enemiesAlive--;
        }

        private void StopWave()
        {
            isWaveActive = false;
        }

        private void UpgradeWaveStats(int numEnemies, float damage, float speed)
        {
            this.numEnemies += numEnemies;
            enemyDamage += damage;
            enemySpeed += speed;
        }

        private void HandleWave()
        {
            if(enemiesLeftToSpawn > 0)
            {
                HandleEnemySpawning();
            }
            else if(enemiesAlive <= 0)
            {
                StopWave();
            }
        }

        private void HandleEnemySpawning()
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= timeBetweenSpawns)
            {
                spawnTimer = 0;

                enemiesLeftToSpawn--;
                enemiesAlive++;

                enemySpawner.SpawnEnemy((int)enemyDamage, enemySpeed);
            }
        }
    }
}