using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerInventory inventory;
        private PlayerStats playerStats;
        private PlayerController playerController;
        private PlayerHUD hud;

        private CameraController cameraController;

        private void Awake()
        {
            inventory = GetComponent<PlayerInventory>();   
            playerStats = GetComponent<PlayerStats>();
            playerController = GetComponent<PlayerController>();

            cameraController = GetComponentInChildren<CameraController>();
        }

        private void Start()
        {
            hud = PlayerHUD.Instance;
            hud.Init(inventory);

            inventory.WeaponChanged += hud.UpdateAmmoInfo;
        }

        private void Update()
        {
            if (playerStats.isAlive)
            {
                playerController.Tick();
                cameraController.Tick();
            }
        }
    }
}