using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class EnemyStats : CharacterStats
    {
        public int points = 5;

        public PlayerPoints playerWhoLastDamaged;

        public override void Die()
        {
            animator.SetTrigger("Death");
            isAlive = false;

            GameManager.Instance.OnEnemyDeath(this, playerWhoLastDamaged);

            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());

            Destroy(gameObject, 10f);
        }
    }
}