using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class PlayerInventory : MonoBehaviour
    {
        public Action WeaponChanged;

        [SerializeField] private WeaponSlot[] weaponSlots;
        [SerializeField] private WeaponData emptyWeaponData;
        [SerializeField] private WeaponPosition activeWeaponPosition;

        private InputManager input;

        private void Start()
        {
            input = InputManager.Instance;

            activeWeaponPosition = WeaponPosition.Primary;
            InitializeWeaponSlots();
        }

        private void Update()
        {
            if(input.swapWeapon)
            { 
                SwapWeaponSlots();
                WeaponChanged?.Invoke();
            }
        }

        public void LoadWeaponOnActiveSlot(WeaponData weaponData)
        {
            weaponSlots[(int)activeWeaponPosition].LoadWeapon(weaponData);
            WeaponChanged?.Invoke();
        }

        public Weapon GetCurrentWeapon()
        {
            return weaponSlots[(int)activeWeaponPosition].weapon;
        }

        private void InitializeWeaponSlots()
        {
            for(int i = 0; i < weaponSlots.Length; i++)
            {
                weaponSlots[i].LoadWeapon(emptyWeaponData);
                weaponSlots[i].SetEnabled(i == (int)activeWeaponPosition);
            }

            WeaponChanged?.Invoke();
        }

        private void SelectWeaponSlot(WeaponPosition weaponPosition)
        {
            for(int i = 0; i < weaponSlots.Length; i++)
            {
                weaponSlots[i].SetEnabled(i == (int)weaponPosition);
            }
        }

        private void SwapWeaponSlots()
        {
            if(activeWeaponPosition == WeaponPosition.Primary)
            {
                activeWeaponPosition = WeaponPosition.Secondary;
            }
            else
            {
                activeWeaponPosition = WeaponPosition.Primary;
            }

            SelectWeaponSlot(activeWeaponPosition);
        }
    }

    public enum WeaponPosition { Primary = 0, Secondary = 1 }
}