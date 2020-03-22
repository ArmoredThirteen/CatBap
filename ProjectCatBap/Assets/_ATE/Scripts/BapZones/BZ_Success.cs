using ATE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Baps
{
	public class BZ_Success : BapZone
	{
        // Points added to bappee points
        public int pointsAdditional = 0;
        // How much to multiply points by, after adding
        public int pointsMultiplier = 1;


        protected override void OnTriggered(Bappee bappee)
        {
            int totalPoints = (bappee.points + pointsAdditional) * pointsMultiplier;
            GS_Events.Invoke (EventID.AddPoints, totalPoints);
        }

    }
}