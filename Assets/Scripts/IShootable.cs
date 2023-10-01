using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public interface IShootable
    {
        void OnHit(Vector3 direction, Vector3 hitPoint, int damage, PlayerPoints player);
        void GetBulletImpact(ref GameObject bulletImpact);
    }
}