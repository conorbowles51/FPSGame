using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class PlayerPickup : MonoBehaviour
    {
        [SerializeField] private float pickupRange = 5f;
        [SerializeField] private LayerMask pickupLayer;

        private PlayerInventory inventory;
        private InputManager input;

        private Camera cam;

        private void Start()
        {
            inventory = GetComponent<PlayerInventory>();
            input = InputManager.Instance;
            cam = GetComponentInChildren<Camera>();
        }

        private void Update()
        {
            if(input.reload)
            {
                PickupWeapon();
            }
        }

        private void PickupWeapon()
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickupRange, pickupLayer))
            {
                WeaponPickup pickup = hit.transform.GetComponent<WeaponPickup>();

                inventory.LoadWeaponOnActiveSlot(pickup.weaponData);

                Destroy(hit.transform.gameObject);
            }
        }
    }
}