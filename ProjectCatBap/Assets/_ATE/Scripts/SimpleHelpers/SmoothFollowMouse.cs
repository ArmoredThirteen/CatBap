using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.SimpleHelpers
{
	public class SmoothFollowMouse : MonoBehaviour
	{
        public float maxDistance = 1;
        public float speed = 1;
        public AnimationCurve distanceBySpeed = new AnimationCurve (new Keyframe (0, 1), new Keyframe (1, 1));


        private void LateUpdate()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            Vector2 moveDir = (mousePos - (Vector2)transform.position).normalized;

            float mouseDist = Vector2.Distance (mousePos, (Vector2)transform.position);
            float moveSpeed = distanceBySpeed.Evaluate (mouseDist / maxDistance) * speed * Time.deltaTime;
            
            transform.position += (Vector3)moveDir * Mathf.Min (moveSpeed, mouseDist);
        }
        
    }
}