using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATE.Events;
using ATE.Baps.Zones;

namespace ATE.Baps
{
	public class Bappee : MonoBehaviour
	{
        // How much noise to send based on movement
        public float noise = 1;
        // Noise less than minNoise won't send
        public float minTrackedNoise = 0.1f;

        // Base points for getting object to desired location
        public int points = 5;


        private Vector2 lastPos;


        private void Start()
        {
            lastPos = transform.position;
        }

        private void Update()
        {
            if (lastPos == (Vector2)transform.position)
                return;

            float noiseAmount = noise * ((Vector2)transform.position - lastPos).magnitude;
            lastPos = transform.position;

            if (noiseAmount >= minTrackedNoise)
                GS_Events.Invoke (EventID.AddNoise, noiseAmount);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            BapZone zone = collision.GetComponent<BapZone> ();
            if (zone != null)
                zone.ProcessBappee (this);
        }

        // Removes the bappee, could be straight deletion or (in the future) things like adding to pool
        public void Remove()
        {
            GameObject.Destroy (gameObject);
        }

    }
}