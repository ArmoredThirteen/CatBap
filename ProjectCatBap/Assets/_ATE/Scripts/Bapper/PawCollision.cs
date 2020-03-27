using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Baps
{
	public class PawCollision : MonoBehaviour
	{
        public float castRadius = 1;
        public float castDist = 1;

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
        }


        //TODO: Algorithm isn't quite right, and overall Bapper vs PawCollision has
        //      really weird separation of powers
        public void BappyBap(float bapForce)
        {
            // Bap vector is largely taken from how quickly paw is rotating left or right
            float leftRightMult = bapForce < 0 ? -bapForceLeft : bapForceRight;
            Vector2 bapVect = transform.right * leftRightMult;

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

            //Vector2 castDir = bapVect + moveVect + bapForwardVect;
            Vector2 castDir = bapVect;

            // Vector2 origin, float radius, Vector2 direction, float distance
            RaycastHit2D[] hits = Physics2D.CircleCastAll (transform.position, castRadius, castDir, castDist);

            for (int i = 0; i < hits.Length; i++)
            {
                Bappee hitObj = hits[i].collider.GetComponent<Bappee> ();
                if (hitObj == null)
                    continue;

                Vector2 forceVect = ((Vector2)hitObj.transform.position - hits[i].point).normalized * castDir.magnitude;

                hits[i].collider.attachedRigidbody.AddForceAtPosition (forceVect, hits[i].point);
            }
        }
		
	}
}