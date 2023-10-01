using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class SinWaveMovement : MonoBehaviour
    {
        [SerializeField] private float intensity;
        [SerializeField] private float time;

        [SerializeField] private bool xAxis;
        [SerializeField] private bool yAxis;
        [SerializeField] private bool zAxis;

        private Vector3 originalPosition;

        private void Start()
        {
            originalPosition = transform.position;
        }

        private void Update()
        {
            float x = xAxis ? originalPosition.x + Mathf.Sin(Time.time * time) * intensity : originalPosition.x;
            float y = yAxis ? originalPosition.y + Mathf.Sin(Time.time * time) * intensity : originalPosition.y;
            float z = zAxis ? originalPosition.z + Mathf.Sin(Time.time * time) * intensity : originalPosition.z;

            Vector3 newPos = new Vector3(x, y, z);

            transform.position = newPos;
        }
    }
}