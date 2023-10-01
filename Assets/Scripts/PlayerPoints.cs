using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class PlayerPoints : MonoBehaviour
    {
        public int currentPoints = 0;

        private PlayerHUD playerHUD;

        private void Start()
        {
            playerHUD = PlayerHUD.Instance;
        }

        public void AddPoints(int numPoints)
        {
            currentPoints += numPoints;
            playerHUD.UpdatePointsInfo(currentPoints);
        }

        public void RemovePoints(int numPoints)
        {
            currentPoints -= numPoints;
            playerHUD.UpdatePointsInfo(currentPoints);
        }
    }
}