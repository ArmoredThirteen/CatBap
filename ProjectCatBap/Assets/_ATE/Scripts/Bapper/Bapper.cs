using ATE.SimpleHelpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Bapper
{
    // Click/hold at location
    // Arm moves to location
    // Baps happen continually
    // Frequency and power of baps increases over time to max
    // Paw rotation amount and direction is based on both
    //    direction of arm movement and weighted randomness
    // Release click, arm begins reset and other arm is active
    // Arm can become re-activated before fully reset
	public class Bapper : MonoBehaviour
    {
        public Transform mouseObject = null;
        public Transform armToMove = null;

        public float maxReach = 5;
        public float maxClickTime = 1;
        public float resetTime = 1;

        public AnimationCurve reachTimeByDistanceFromStart = new AnimationCurve (new Keyframe (0, 0), new Keyframe (1, 1));


        private Vector2 startPos;

        private Vector2 startPosSnapshot;
        private Vector2 targetPosSnapshot;

        private bool isMovingForward = false;
        private float timeClicked = 0;


        public bool IsActive
        {
            get; set;
        }


        private void Awake()
        {
            startPos = transform.position;
        }

        private void FixedUpdate()
        {
            // Set timeClicked
            // The smooth follow mouse object gets toggled on/off
            //  This prevents stutter when double-clicking and moving mouse rapidly
            if (IsActive && Input.GetMouseButton (0))
            {
                timeClicked = Mathf.Min (maxClickTime, timeClicked + Time.deltaTime);
                mouseObject.gameObject.SetActive (true);
            }
            else
            {
                timeClicked = Mathf.Max (0, timeClicked - Time.deltaTime);
                mouseObject.gameObject.SetActive (timeClicked <= 0);
            }

            // Rotate to face smooth mouse
            transform.up = (Vector2)mouseObject.position - startPos;

            // When at rest, no need to do anything fancy for movement
            if (timeClicked <= 0)
            {
                armToMove.localPosition = Vector3.zero;
                return;
            }

            // Find distance of target object and determine how far along that distance to be based on time clicked
            float targetDistance = Mathf.Min (maxReach, Vector2.Distance (startPos, mouseObject.position));
            float distanceFromTime = targetDistance * reachTimeByDistanceFromStart.Evaluate (timeClicked / maxClickTime);

            // Move arm toward target
            armToMove.localPosition = Vector3.up * Mathf.Min (targetDistance, distanceFromTime);
        }

        private void BapForward()
        {
            float targetDistance = Mathf.Min (maxReach, Vector2.Distance (startPos, mouseObject.position));
            float distanceFromTime = targetDistance * reachTimeByDistanceFromStart.Evaluate (timeClicked / maxClickTime);

            armToMove.localPosition = Vector3.up * Mathf.Min (targetDistance, distanceFromTime);
        }

        private void BapReset()
        {
            float targetDistance = Mathf.Min (maxReach, Vector2.Distance (startPos, mouseObject.position));
            float distanceFromTime = targetDistance * reachTimeByDistanceFromStart.Evaluate (timeClicked / maxClickTime);

            armToMove.localPosition = Vector3.up * Mathf.Min (targetDistance, distanceFromTime);
        }

    }
}