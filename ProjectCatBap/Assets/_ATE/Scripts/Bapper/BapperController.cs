using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Bapper
{
	public class BapperController : MonoBehaviour
	{
        public Bapper bapperLeft = null;
        public Bapper bapperRight = null;

        
        public void Start()
        {
            ActivateLeft ();
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown (0))
                ToggleActive ();
        }


        private void ToggleActive()
        {
            bapperLeft.IsActive = !bapperLeft.IsActive;
            bapperRight.IsActive = !bapperRight.IsActive;
        }

        private void ActivateLeft()
        {
            bapperLeft.IsActive = true;
            bapperRight.IsActive = false;
        }

        private void ActivateRight()
        {
            bapperLeft.IsActive = false;
            bapperRight.IsActive = true;
        }

    }
}