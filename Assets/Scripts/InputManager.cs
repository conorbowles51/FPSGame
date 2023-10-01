using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        private PlayerControls playerControls;

        public Vector2 move { get; private set; }
        public Vector2 look { get; private set; }
        
        public bool jump { get; private set; }
        public bool sprint { get; private set; }

        public bool reload { get; private set; }
        public bool swapWeapon { get; private set; }

        public bool attackAuto { get; private set; }
        public bool attackSemiAuto { get; private set; }

        public bool ads { get; private set; }

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            SetUpPlayerControls();
        }

        private void LateUpdate()
        {
            jump = false;
            sprint = false;
            reload = false;
            swapWeapon = false;
            attackSemiAuto = false;
        }

        private void SetUpPlayerControls()
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Move.performed += playerControls => move = playerControls.ReadValue<Vector2>();
            playerControls.PlayerMovement.Look.performed += i => look = i.ReadValue<Vector2>();

            playerControls.PlayerMovement.Jump.performed += playerControls => jump = true;
            playerControls.PlayerMovement.Sprint.performed += playerControls => sprint = true;

            playerControls.PlayerActions.Reload.performed += playerControls => reload = true;
            playerControls.PlayerActions.SwapWeapon.performed += playerControls => swapWeapon = true;

            playerControls.PlayerAttack.Attack.performed += playerControls => attackAuto = attackSemiAuto = true;
            playerControls.PlayerAttack.Attack.canceled += playerControls => attackAuto = false;

            playerControls.PlayerActions.ADS.performed += playerControls => ads = true;
            playerControls.PlayerActions.ADS.canceled += playerControls => ads = false;

            playerControls.Enable();
        }
    }
}