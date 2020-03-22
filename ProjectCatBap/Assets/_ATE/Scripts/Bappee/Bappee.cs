using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATE.Events;

namespace ATE.Baps
{
	public class Bappee : MonoBehaviour
	{
        public float noise = 1;
        public int points = 0;


        private Vector2 lastPos;


        private void Start()
        {
            lastPos = transform.position;
        }

        private void Update()
        {
            if (lastPos == (Vector2)transform.position)
                return;

            float moveAmount = ((Vector2)transform.position - lastPos).magnitude;
            lastPos = transform.position;

            GS_Events.Invoke (EventID.AddNoise, moveAmount * noise);
        }

    }
}