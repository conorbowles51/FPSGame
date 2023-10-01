using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float jumpForce;

        [Header("Ground Detection Settings")]
        [SerializeField] private float gravity;
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask groundMask;

        private InputManager input;
        private CharacterController controller;

        private Vector3 moveDirection;
        private Vector3 velocity;

        private bool isGrounded = true;
        [HideInInspector] public bool isSprinting = false;

        private void Start()
        {
            input = InputManager.Instance;
            controller = GetComponent<CharacterController>();
        }

        public void Tick()
        {
            float delta = Time.deltaTime;

            PerformGroundCheck();
            ApplyGravity(delta);
            DecideMoveSpeed();
            HandleMovement(delta);
            HandleJumping();
        }

        private void PerformGroundCheck()
        {
            isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        }

        private void ApplyGravity(float delta)
        {
            velocity.y += gravity * delta;

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            controller.Move(velocity * delta);
        }

        private void DecideMoveSpeed()
        {
            isSprinting = (input.sprint || isSprinting) && input.move != Vector2.zero;

            if (isSprinting)
            {
                moveSpeed = sprintSpeed;
            }
            else
            {
                moveSpeed = runSpeed;
            }
        }

        private void HandleMovement(float delta)
        {
            moveDirection = new Vector3(input.move.x, 0, input.move.y);
            moveDirection.Normalize();
            moveDirection = transform.TransformDirection(moveDirection);

            controller.Move(moveDirection * moveSpeed * delta);
        }

        private void HandleJumping()
        {
            if (input.jump && isGrounded)
            {
                velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }
    }
}