using ATE.Baps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE
{
	public class CastTest : MonoBehaviour
	{
        public Transform castToward = null;
        public float castTime = 1;
        public float castRadius = 1;
        public float castDist = 5;
        public float hitForce = 5;


        private float timer_cast = 0;


        private void Start()
        {
            timer_cast = castTime;
        }

        private void Update()
        {
            timer_cast -= Time.deltaTime;
            if (timer_cast > 0)
                return;

            Debug.Log ("Force");
            timer_cast = castTime;

            // Vector2 origin, float radius, Vector2 direction, float distance
            Vector2 castDir = castToward.position - transform.position;
            RaycastHit2D[] hits = Physics2D.CircleCastAll (transform.position, castRadius, castDir, castDist);

            for (int i = 0; i < hits.Length; i++)
            {
                Bappee hitObj = hits[i].collider.GetComponent<Bappee> ();
                if (hitObj == null)
                    continue;

                //Vector2 forceVect = (hits[i].point - (Vector2)transform.position).normalized * hitForce;
                //Vector2 forceVect = castDir.normalized * hitForce;
                Vector2 forceVect = ((Vector2)hitObj.transform.position - hits[i].point).normalized * hitForce;

                hits[i].collider.attachedRigidbody.AddForceAtPosition (forceVect, hits[i].point);
            }
        }

    }
}