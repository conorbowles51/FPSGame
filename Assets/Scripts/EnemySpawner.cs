using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance;

        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private GameObject[] enemies;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }
        

        public void SpawnEnemy(int enemyDamage, float enemySpeed)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length - 1);
            int enemyIndex = Random.Range(0, enemies.Length - 1);

            EnemyAI enemy = Instantiate(enemies[enemyIndex], transform).GetComponent<EnemyAI>();

            enemy.Init(enemyDamage, enemySpeed, spawnPoints[spawnIndex].position);
        }
    }
}