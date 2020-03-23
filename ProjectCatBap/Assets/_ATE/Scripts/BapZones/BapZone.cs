using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Baps.Zones
{
    // Target zone that can be triggered by a Bappee
    [RequireComponent(typeof(Collider2D))]
	public abstract class BapZone : MonoBehaviour
	{
        public bool isEnabled = true;

        public bool removeBappee = false;
        public int disableAfterProcesses = -1;


        private int timesProcessed = 0;


        public void ProcessBappee(Bappee bappee)
        {
            if (!isEnabled)
                return;

            OnTriggered (bappee);

            timesProcessed++;
            if (disableAfterProcesses > 0 && timesProcessed >= disableAfterProcesses)
                isEnabled = false;

            if (removeBappee)
                bappee.Remove ();
        }

        protected abstract void OnTriggered(Bappee bappee);
		
	}
}