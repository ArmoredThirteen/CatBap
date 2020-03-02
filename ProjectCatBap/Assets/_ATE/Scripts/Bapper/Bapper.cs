using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE
{
    // Concept #1
    // Receive click in location
    // Move arm so hand lines up with bapLoc
    // When near end, begin rotating paw back
    // At location, paw is fully rotated
    // Either time out and reset arm to start location
    //    or bap down and to the side
    // Either animation completes and then arm resets
    //    or receive click and perform another bap
    //    (repeat)
    // When arm begins reset (while still extended)
    //    Unlock other to be active bapper

    // Concept #2 (current chosen)
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
        public Transform faceToObject = null;

        public Transform armToMove = null;

        public float maxReachDistance = 5;

        public float speedMoveToPosition = 1;
        public float speedMoveToStartPos = 1;

        public float speedRotateFaceMouse = 1;
        public float speedRotateToStartRot = 1;


        private Vector2 startPos;
        private Quaternion startRot;


        private void Awake()
        {
            startPos = transform.position;
            startRot = transform.rotation;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton (0))
                BapTowardMouse (speedMoveToPosition * Time.deltaTime, speedRotateFaceMouse * Time.deltaTime);
            //else if (Input.GetMouseButton (1))
            else
                ResetBapper (speedMoveToStartPos * Time.deltaTime, speedRotateToStartRot * Time.deltaTime);

            // Rotate to face smooth mouse
            Vector2 rotDir = ((Vector2)faceToObject.position - (Vector2)transform.position).normalized;
            transform.up = rotDir;
        }

        private void BapTowardMouse(float moveSpeed, float rotateSpeed)
        {
            if (Vector3.Distance (armToMove.position, startPos) >= maxReachDistance)
                return;

            // Move forward
            float moveActual = Mathf.Min (moveSpeed, maxReachDistance - Vector3.Distance (armToMove.position, startPos));
            armToMove.localPosition += Vector3.up * moveActual;
        }

        private void ResetBapper(float moveSpeed, float rotateSpeed)
        {
            if (Vector3.Distance (armToMove.position, startPos) <= 0.05)
                return;

            // Move back to starting position
            float moveActual = Mathf.Min (moveSpeed, Vector3.Distance (armToMove.position, startPos));
            armToMove.localPosition += Vector3.down * moveActual;
        }

    }
}