using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class PlayerAttacker : MonoBehaviour
    {
        private PlayerInventory inventory;
        private InputManager input;
        private PlayerHUD hud;

        private Weapon currentWeapon;

        private void Start()
        {
            inventory = GetComponent<PlayerInventory>();
            input = InputManager.Instance;
            hud = PlayerHUD.Instance;
        }

        private void Update()
        {
            currentWeapon = inventory.GetCurrentWeapon();

            HandleAttack();
            HandleReload();
        }

        private void HandleAttack()
        {
            if (!currentWeapon.IsArmed()) 
                return;

            bool isAuto = currentWeapon.IsAutomatic();
            bool successfullyFired = false;

            if(isAuto && input.attackAuto || 
               !isAuto && input.attackSemiAuto)
            {
                successfullyFired = currentWeapon.RequestFire();
            }

            if (successfullyFired)
                hud.UpdateAmmoInfo();
        }

        private void HandleReload()
        {
            if (!currentWeapon.IsArmed())
                return;

            bool successfullyReloaded = false;

            if (input.reload)
            {
                successfullyReloaded = currentWeapon.RequestReload();
            }

            if (successfullyReloaded)
                hud.UpdateAmmoInfo();
        }
    }
}