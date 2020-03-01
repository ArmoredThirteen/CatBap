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

        public Rigidbody2D armRigidbody = null;
        public Rigidbody2D pawRigidbody = null;

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
                BapTowardMouse (speedMoveToPosition, speedRotateFaceMouse);
            //else if (Input.GetMouseButton (1))
            else
                ResetBapper (speedMoveToStartPos, speedRotateToStartRot);
        }

        private void BapTowardMouse(float moveSpeed, float rotateSpeed)
        {
            moveSpeed *= Time.deltaTime;
            rotateSpeed *= Time.deltaTime;

            //TODO: Should go to offset location that is placed near paw/wrist
            //TODO: Duplicate in ResetBapper()
            /*Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            Vector2 moveDir = (mousePos - (Vector2)transform.position).normalized;
            float mouseDist = Vector2.Distance (mousePos, (Vector2)transform.position);
            transform.position += (Vector3)moveDir * Mathf.Min (moveSpeed, mouseDist);*/

            /*targetRotation = Quaternion.LookRotation (target.position - transform.position);
            str = Mathf.Min (strength * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, str);*/
            /*Quaternion targetRot = Quaternion.LookRotation (mousePos - (Vector2)transform.position);
            float rotStr = Mathf.Min (rotateSpeed, 1);
            transform.rotation = Quaternion.Lerp (transform.rotation, targetRot, rotStr);*/

            //Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            Vector2 rotDir = ((Vector2)faceToObject.position - (Vector2)transform.position).normalized;
            transform.up = rotDir;
        }

        private void ResetBapper(float moveSpeed, float rotateSpeed)
        {
            //TODO: Should go to offset location that is placed near paw/wrist
            //TODO: Duplicate in BapTowardMouse()
            Vector2 moveDir = (startPos - (Vector2)transform.position).normalized;
            float mouseDist = Vector2.Distance ((Vector2)transform.position, startPos);
            transform.position += (Vector3)moveDir * Mathf.Min (moveSpeed, mouseDist);
        }

    }
}