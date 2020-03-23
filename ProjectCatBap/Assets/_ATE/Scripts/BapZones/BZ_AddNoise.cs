using ATE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Baps.Zones
{
	public class BZ_AddNoise : BapZone
	{
        // Amount of noise to add
        public int noise = 0;


        protected override void OnTriggered(Bappee bappee)
        {
            GS_Events.Invoke (EventID.AddNoise, noise);
        }

    }
}