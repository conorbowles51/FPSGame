using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class BallisticsManager : MonoBehaviour
    {
        public static BallisticsManager Instance;

        [SerializeField] private Transform bulletContainer;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject defaultBulletImpactPrefab;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        public void Fire(Vector3 start, Vector3 dir, float range, int damage, PlayerPoints player)
        {
            Vector3 end = start + dir * 100;
            RaycastHit hit;

            GameObject bulletImpactPrefab = null;

            if (Physics.Raycast(start, dir, out hit, range))
            {
                end = hit.point;

                IShootable shootable = hit.transform.GetComponent<IShootable>();

                if (shootable != null)
                {
                    shootable.OnHit(dir, hit.point, damage, player);
                    shootable.GetBulletImpact(ref bulletImpactPrefab);
                }
            }

            GameObject bullet = Instantiate(bulletPrefab, bulletContainer);
            GameObject bulletImpact = Instantiate((bulletImpactPrefab == null) ? defaultBulletImpactPrefab : bulletImpactPrefab, 
                                                   end, Quaternion.Euler(-dir), bulletContainer);

            BulletTrail trail = bullet.GetComponent<BulletTrail>();
            trail.Init(start, end);

            Destroy(bullet, 0.1f);
            Destroy(bulletImpact, 1f);
        }
    }
}