using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class ShootableEnemy : MonoBehaviour, IShootable
    {
        [SerializeField] private GameObject bloodSplatter;

        private EnemyStats enemyStats;

        private void Awake()
        {
            enemyStats = GetComponent<EnemyStats>();
        }

        public void GetBulletImpact(ref GameObject bulletImpact)
        {
            if (bloodSplatter != null)
                bulletImpact = bloodSplatter;
        }

        public void OnHit(Vector3 direction, Vector3 hitPoint, int damage, PlayerPoints player)
        {
            enemyStats.TakeDamage(damage);
            enemyStats.playerWhoLastDamaged = player;
        }
    }
}