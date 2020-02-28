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
        //public GameObject arm = null;
        //public GameObject paw = null;

        public Rigidbody2D armRigidbody = null;
        public Rigidbody2D pawRigidbody = null;

        public float moveToSpeed = 1;
        

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown (0))
                BapTowardMouse ();
            else
                ResetBapper ();
        }

        private void BapTowardMouse()
        {

        }

        private void ResetBapper()
        {

        }

    }
}