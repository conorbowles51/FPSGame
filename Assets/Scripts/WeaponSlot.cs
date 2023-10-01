using CB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public Weapon weapon { get; private set; }

    private GameObject weaponGO;

    public void LoadWeapon(WeaponData weaponData)
    {
        if (weaponGO != null)
            Destroy(weaponGO);

        weaponGO = Instantiate(weaponData.weaponModel, transform);

        weapon = weaponGO.AddComponent<Weapon>();
        weapon.Init(weaponData, weaponGO.transform.Find("MuzzleTransform"));
    }

    public void SetEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}
