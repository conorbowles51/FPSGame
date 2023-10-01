using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private WaveManager waveManager;

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
            waveManager = WaveManager.Instance;
        }

        private void Update()
        {
            if(!waveManager.isWaveActive)
            {
                waveManager.BeginNextWave();
            }
        }

        public void OnEnemyDeath(EnemyStats enemyStats, PlayerPoints playerPoints)
        {
            waveManager.RecordEnemyDeath();
            playerPoints.AddPoints(enemyStats.points);
        }
    }
}