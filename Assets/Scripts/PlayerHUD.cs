using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CB
{
    public class PlayerHUD : MonoBehaviour
    {
        public static PlayerHUD Instance;

        [SerializeField] private TextMeshProUGUI currentAmmoText;
        [SerializeField] private TextMeshProUGUI reserveAmmoText;

        [SerializeField] private TextMeshProUGUI currentPointsText;

        [SerializeField] private TextMeshProUGUI waveText;

        [SerializeField] private Image bloodOverlay;

        private PlayerInventory inventory;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        public void Init(PlayerInventory playerInventory)
        {
            inventory = playerInventory;
        }

        public void UpdateAmmoInfo()
        {
            currentAmmoText.text = inventory.GetCurrentWeapon().currentAmmo.ToString();
            reserveAmmoText.text = inventory.GetCurrentWeapon().reserveAmmo.ToString();
        }

        public void UpdateWaveInfo(int waveNum)
        {
            waveText.text = waveNum.ToString();
        }

        public void UpdatePointsInfo(int points)
        {
            currentPointsText.text = points.ToString();
        }

        public void UpdateHealth(float currentHealth, int maxHealth)
        {
            float alpha = 1 - (currentHealth / maxHealth);
            bloodOverlay.color = new Color(1, 1, 1, alpha);
        }
    }
}