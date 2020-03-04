using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Bapper
{
	public class PawCollision : MonoBehaviour
	{
        public float bapForce = 0;


        private Collider2D thisCollider = null;


        private void Awake()
        {
            thisCollider = this.gameObject.GetComponent<Collider2D> ();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (bapForce <= 0)
                return;

            Vector2 collisionPoint = collision.GetContact (0).point;
            Vector2 forceVector = (collisionPoint - (Vector2)thisCollider.transform.position).normalized * bapForce;

            collision.collider.attachedRigidbody.AddForceAtPosition (forceVector, collisionPoint);
        }
		
	}
}