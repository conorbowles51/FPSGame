using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShootableObject : MonoBehaviour, IShootable
    {
        [SerializeField] private float hitForce = 10f;
        [SerializeField] private GameObject bulletImpact;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void GetBulletImpact(ref GameObject bulletImpact)
        {
            if (this.bulletImpact != null)
                bulletImpact = this.bulletImpact;
        }

        public void OnHit(Vector3 direction, Vector3 hitPoint, int damage, PlayerPoints player)
        {
            rb.AddForceAtPosition(direction * hitForce, hitPoint);
        }
    }
}