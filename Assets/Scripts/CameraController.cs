using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float sensitivity;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform pivotTransform;

        private InputManager input;

        private float xRot;

        private void Start()
        {
            input = InputManager.Instance;
        }

        public void Tick()
        {
            float xValue = input.look.x * sensitivity * Time.deltaTime;
            float yValue = input.look.y * sensitivity * Time.deltaTime;

            xRot -= yValue;
            xRot = Mathf.Clamp(xRot, -90, 90);

            pivotTransform.localRotation = Quaternion.Euler(new Vector3(xRot, 0, 0));
            playerTransform.Rotate(new Vector3(0, xValue, 0));
        }
    }
}