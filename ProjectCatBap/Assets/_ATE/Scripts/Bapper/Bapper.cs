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
        // Target to face toward
        public Transform followObj = null;
        // Object to rotate to look at followObj
        public Transform rootObj = null;
        // Object to move forward while click is held
        public Transform armObj = null;
        // Object to rotate erratically while bap is active
        public Transform wristObj = null;

        public float maxReach = 5;
        public float timeToMaxReach = 1;

        public AnimationCurve clickTimeByDistanceFromStart = new AnimationCurve (new Keyframe (0, 0), new Keyframe (1, 1));


        private float timeClicked = 0;
        private float timeFullExtended = 0;

        private float wristTargetRot = 0;
        private float wristRotSpeed = 0;
        private float timerCurrWristBap = 0;


        public bool IsActive
        {
            get; set;
        }


        private void FixedUpdate()
        {
            if (IsActive && Input.GetMouseButton (0))
                ActiveBapping ();
            else
                ResettingBapper ();

            RotateRootToFollow ();
            MoveArmAlongCurve ();
            BapWrist ();
        }


        private void ActiveBapping()
        {
            timeClicked = Mathf.Min (timeToMaxReach, timeClicked + Time.deltaTime);
            if (timeClicked >= timeToMaxReach)
                timeFullExtended += Time.deltaTime;

            followObj.gameObject.SetActive (true);
        }

        private void ResettingBapper()
        {
            timeClicked = Mathf.Max (0, timeClicked - Time.deltaTime);
            timeFullExtended = 0;

            followObj.gameObject.SetActive (timeClicked <= 0);
        }

        private void RotateRootToFollow()
        {
            // Directly face smooth mouse
            rootObj.up = (Vector2)followObj.position - (Vector2)rootObj.position;
        }

        private void MoveArmAlongCurve()
        {
            // When at rest, no need to do anything fancy for movement
            if (timeClicked <= 0)
            {
                armObj.localPosition = Vector3.zero;
                return;
            }

            // Find distance of target object and determine how far along that distance to be based on time clicked
            float targetDistance = Mathf.Min (maxReach, Vector2.Distance (rootObj.position, followObj.position));
            float distanceFromTime = targetDistance * clickTimeByDistanceFromStart.Evaluate (timeClicked / timeToMaxReach);

            // Move arm toward target
            armObj.localPosition = Vector3.up * Mathf.Min (targetDistance, distanceFromTime);
        }

        private void BapWrist()
        {
            if (timeFullExtended <= 0)
                return;

            timerCurrWristBap -= Time.deltaTime;
            if (timerCurrWristBap <= 0)
            {
                //TODO: Hard-coding
                wristTargetRot = (Random.value * 135) - 20;
                wristRotSpeed = (Random.value * 5) + 5;
                timerCurrWristBap = Random.value / 2;
            }

            wristObj.rotation = Quaternion.Lerp (wristObj.rotation, Quaternion.Euler (0, 0, wristTargetRot), Time.deltaTime * wristRotSpeed);
        }

    }
}