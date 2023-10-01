using CB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 playerDeathRotation = new Vector3(45, 0, 75);
    [SerializeField] private float deathTime = 3f;

    private InputManager input;
    private PlayerInventory inventory;
    private PlayerStats playerStats;
    private PlayerController playerController;

    private WeaponData currentWeaponData;
    private Transform modelTransform;
    private Vector3 defaultModelPosition;

    private Camera cam;
    private float defaultFov;

    private bool isAimingDownSights = false;

    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        playerStats = GetComponent<PlayerStats>(); 
        playerController = GetComponent<PlayerController>();
        inventory.WeaponChanged += UpdateAnimator;
    }

    private void Start()
    {
        input = InputManager.Instance;

        cam = GetComponentInChildren<Camera>();
        defaultFov = cam.fieldOfView;
    }

    private void Update()
    {
        bool sprinting = playerController.isSprinting;

        if(!playerStats.isAlive)
        {
            HandleDeath();
            return;
        }

        if(!isAimingDownSights)        
            HandleSprinting(sprinting);

        HandleAds();
        HandleSway();
        HandleGunBob(sprinting);
    }

    private void UpdateAnimator()
    {
        ResetModelTransform();
        GetNewModelTransform();
    }

    private void ResetModelTransform()
    {
        if (modelTransform == null)
            return;

        modelTransform.localPosition = defaultModelPosition;
    }

    private void GetNewModelTransform()
    {
        modelTransform = inventory.GetCurrentWeapon().transform;
        defaultModelPosition = modelTransform.localPosition;

        currentWeaponData = inventory.GetCurrentWeapon().GetWeaponData();
    }

    private void HandleAds()
    {
        float xValue = modelTransform.localPosition.x;
        float fov = cam.fieldOfView;

        if (currentWeaponData.canAds)
        {
            if (input.ads)
            {
                xValue = Mathf.Lerp(xValue, currentWeaponData.adsXValue, currentWeaponData.adsSpeed * Time.deltaTime);
                fov = Mathf.Lerp(fov, currentWeaponData.adsFov, currentWeaponData.adsSpeed * Time.deltaTime);

                isAimingDownSights = true;
            }
            else
            {
                xValue = Mathf.Lerp(xValue, defaultModelPosition.x, currentWeaponData.adsSpeed * Time.deltaTime);
                fov = Mathf.Lerp(fov, defaultFov, currentWeaponData.adsSpeed * Time.deltaTime);

                isAimingDownSights = false;
            }
        }
        else
        {
            xValue = Mathf.Lerp(xValue, defaultModelPosition.x, 40 * Time.deltaTime);
            fov = Mathf.Lerp(fov, defaultFov, 40 * Time.deltaTime);

            isAimingDownSights = false;
        }
        
        Vector3 newPosition = modelTransform.localPosition;
        newPosition.x = xValue;
        modelTransform.localPosition = newPosition;

        cam.fieldOfView = fov;
    }

    private void HandleSway()
    {
        float xValue = input.look.x * currentWeaponData.swayIntensity;
        float yValue = input.look.y * currentWeaponData.swayIntensity;

        Quaternion xRot = Quaternion.AngleAxis(-yValue, Vector3.right);
        Quaternion yRot = Quaternion.AngleAxis(xValue, Vector3.up);

        Quaternion targetRot = xRot * yRot;

        modelTransform.localRotation = Quaternion.Slerp(modelTransform.localRotation, targetRot, currentWeaponData.swaySmoothness * Time.deltaTime);
    }

    private void HandleGunBob(bool sprinting)
    {
        float intensityMultiplier = sprinting ? 2 : 1;
        intensityMultiplier = isAimingDownSights ? intensityMultiplier * currentWeaponData.adsBobMultiplier : intensityMultiplier;

        float speedMultiplier = sprinting ? 3 : 1;

        float yOffset = Mathf.Sin(Time.time * currentWeaponData.gunBobSpeed * speedMultiplier) 
                                 * currentWeaponData.gunBobIntensity * intensityMultiplier;

        Vector3 localPos = modelTransform.localPosition;
        localPos.y = defaultModelPosition.y + yOffset;
        modelTransform.localPosition = localPos;
    }

    private void HandleSprinting(bool sprinting)
    {
        Vector3 localRot = modelTransform.localEulerAngles;

        if(sprinting)
        {
            float yRot = (currentWeaponData.sprintOffset 
                        + Mathf.Sin(Time.time * currentWeaponData.sprintAnimSpeed))
                        * currentWeaponData.sprintAnimIntensity;
            localRot.y = yRot;
        }

        modelTransform.localEulerAngles = localRot;
    }

    private void HandleDeath()
    {
        Vector3 rotation = playerTransform.rotation.eulerAngles;

        rotation = Vector3.Slerp(rotation, playerDeathRotation, deathTime * Time.deltaTime);

        playerTransform.rotation = Quaternion.Euler(rotation);
    }
}
