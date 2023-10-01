using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB 
{
    public class Weapon : MonoBehaviour
    {
        public int currentAmmo { get; private set; }
        public int reserveAmmo { get; private set; }

        private BallisticsManager ballistics;
        private WeaponData weaponData;

        private Transform muzzleTransform;

        private float lastTimeFired;

        public void Init(WeaponData weaponData, Transform muzzle)
        {
            ballistics = BallisticsManager.Instance;
            this.weaponData = weaponData;

            muzzleTransform = muzzle;

            currentAmmo = weaponData.magazineSize;
            reserveAmmo = weaponData.magazineSize * (weaponData.magazineCount - 1);
        }
        
        public bool RequestFire()
        {
            if(Time.time - lastTimeFired >= weaponData.fireRate 
               && currentAmmo > 0)
            {
                lastTimeFired = Time.time;
                currentAmmo--;

                ballistics.Fire(muzzleTransform.position, muzzleTransform.forward, weaponData.range, weaponData.damage, GetComponentInParent<PlayerPoints>());
                return true;
            }

            return false;
        }

        public bool RequestReload()
        {
            if(currentAmmo < weaponData.magazineSize && reserveAmmo >= weaponData.magazineSize)
            {
                currentAmmo = weaponData.magazineSize;
                reserveAmmo -= weaponData.magazineSize;

                return true;
            }

            return false;
        }

        public WeaponData GetWeaponData()
        {
            return weaponData;
        }
        public bool IsArmed()
        {
            return weaponData.isArmed;
        }
        public bool IsAutomatic()
        {
            return weaponData.automatic;
        }
    }
}