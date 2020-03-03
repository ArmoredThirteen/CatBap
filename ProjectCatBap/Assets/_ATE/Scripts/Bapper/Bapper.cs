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

        public AnimationCurve reachTimeByDistanceFromStart = new AnimationCurve (new Keyframe (0, 0), new Keyframe (1, 1));


        private Vector2 startPos;

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
            // Rotate to face smooth mouse
            transform.up = (Vector2)mouseObject.position - startPos;

            //TODO: Currently when mouse is close to startPos, then mouse is released and moved away from arm, the arm jumps forward
            //      Could maybe fix by having max time get lowered based on ratio of dist/maxDist
            //      Or make it so returning to starting position has a fixed reset solution instead of using timeClicked curve

            if (IsActive && Input.GetMouseButton (0))
                timeClicked = Mathf.Min (maxClickTime, timeClicked + Time.deltaTime);
            else
                timeClicked = Mathf.Max (0, timeClicked - Time.deltaTime);

            float targetDistance = Mathf.Min (maxReach, Vector2.Distance (startPos, mouseObject.position));
            float distanceFromTime = maxReach * reachTimeByDistanceFromStart.Evaluate (timeClicked / maxClickTime);

            armToMove.localPosition = Vector3.up * Mathf.Min (targetDistance, distanceFromTime);
        }

    }
}