using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class BulletTrail : MonoBehaviour
    {
        [SerializeField] private float startWidth, endWidth;
        [SerializeField] private Color startColor, endColor;
        [SerializeField] private Material trailMaterial;

        public void Init(Vector3 start, Vector3 end)
        {
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            lineRenderer.startWidth = startWidth;
            lineRenderer.startColor = startColor;

            lineRenderer.endWidth = endWidth;
            lineRenderer.endColor = endColor;

            lineRenderer.material = trailMaterial;
        }
    }
}