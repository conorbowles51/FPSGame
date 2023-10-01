using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class PlayerStats : CharacterStats
    {
        [SerializeField] private float healthRegenAmount = 20f;
        [SerializeField] private float timeBeforeHealthRegen = 4f;

        private PlayerHUD hud;

        private float lastTimeDamaged;

        private void Start()
        {
            hud = PlayerHUD.Instance;
        }

        private void Update()
        {
            if(Time.time - lastTimeDamaged > timeBeforeHealthRegen && currentHealth < maxHealth)
            {
                currentHealth += healthRegenAmount * Time.deltaTime;
                
                if(currentHealth > maxHealth)
                    currentHealth = maxHealth;
            }

            hud.UpdateHealth(currentHealth, maxHealth);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            lastTimeDamaged = Time.time;
        }

        public override void Die()
        {
            Debug.Log("Player Dead");
            isAlive = false;
        }
    }
}