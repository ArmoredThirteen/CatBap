using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Points
{
	public class PointsHandler : MonoBehaviour
	{
        public static PointsHandler instance = null;

        public int Points
        {
            private set; get;
        }


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);
        }


        public void AddPoints(int points)
        {
            instance.Points += points;
        }

    }
}