using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Baps
{
	public class PawCollision : MonoBehaviour
	{
        // Duration to keep bap active when triggered
        public float bapDuration = 0.05f;

        // The many types of force multipliers for designers to tune the physics
        public float bapForceLeft    = 1;
        public float bapForceRight   = 1;
        public float bapForceMove    = 1;
        public float bapForceForward = 1;

        // How many move vectors to store for smoother move vector force
        public int moveVectorSmooth = 5;


        // Last known position during Update()
        private Vector2 lastPos = Vector2.zero;
        // Historical movement vectors, not just world position
        private Queue<Vector2> moveVects = new Queue<Vector2> ();

        private float bapForceTimer;


        public float BapForce { get; set; }


        private void Awake()
        {
            lastPos = transform.position;
        }

        private void Update()
        {
            moveVects.Enqueue ((Vector2)transform.position - lastPos);
            if (moveVects.Count > moveVectorSmooth)
                moveVects.Dequeue ();

            lastPos = transform.position;
            bapForceTimer -= Time.deltaTime;
        }


        public void BappyBap(float bapForce)
        {
            BapForce = bapForce;
            bapForceTimer = bapDuration;
        }


        private void OnTriggerStay2D(Collider2D collision)
        {
            if (bapForceTimer <= 0)
                return;

            //TODO: Need to better figure out the BapForce calculations
            // Bap vector is largely taken from how quickly paw is rotating left or right
            float leftRightMult = BapForce < 0 ? -bapForceLeft : bapForceRight;
            Vector2 bapVect = transform.right * leftRightMult;// * BapForce;

            // Movement vector is average of moveVects
            Vector2 moveVect = Vector2.zero;
            for (int i = 0; i < moveVectorSmooth && i < moveVects.Count; i++)
            {
                Vector2 currVect = moveVects.Dequeue ();
                moveVect += currVect;
                moveVects.Enqueue (currVect);
            }
            moveVect = (moveVect / moveVects.Count) * bapForceMove;

            // Manual weight for this axis since the left/right bap limits feels wrong
            Vector2 bapForwardVect = transform.up * bapForceForward;

            // Add it all up and there we go, some crazy force vector
            collision.attachedRigidbody.AddForceAtPosition (bapVect + moveVect + bapForwardVect, transform.position);
        }
		
	}
}