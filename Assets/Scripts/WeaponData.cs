using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    [CreateAssetMenu(menuName = "Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("General")]
        public string weaponName;
        [Tooltip("Ensure a transform named MuzzleTransform exists as child")]
        public GameObject weaponModel;
        public bool isArmed;

        [Header("Ammo")]
        public int magazineSize;
        public int magazineCount;

        [Header("Stats")]
        public bool automatic;
        public int damage;
        public float fireRate;
        public float range;

        [Header("Aiming")]
        public bool canAds;
        public float adsXValue;
        public float adsSpeed;
        public float adsFov;

        [Header("Sway")]
        public float swaySmoothness;
        public float swayIntensity;

        [Header("Gun Bob")]
        public float gunBobSpeed;
        public float gunBobIntensity;
        public float adsBobMultiplier;

        [Header("Sprinting")]
        public float sprintOffset;
        public float sprintAnimSpeed;
        public float sprintAnimIntensity;
    }
}