using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class CharacterStats : MonoBehaviour
    {
        public bool isAlive = true;

        [SerializeField] protected int maxHealth;
        protected float currentHealth;

        protected Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();    
            currentHealth = maxHealth;
        }

        public virtual void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0 && isAlive)
                Die();
        }

        public virtual void Die() { }
    }
}