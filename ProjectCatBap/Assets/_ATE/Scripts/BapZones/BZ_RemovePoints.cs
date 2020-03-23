using ATE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Baps.Zones
{
	public class BZ_RemovePoints : BapZone
	{
        // Points added to bappee points
        public int pointsAdd = 0;
        // How much to multiply points by, after adding
        public int pointsMultiply = 1;


        protected override void OnTriggered(Bappee bappee)
        {
            int totalPoints = (bappee.points + pointsAdd) * pointsMultiply;
            GS_Events.Invoke (EventID.RemovePoints, totalPoints);
        }

    }
}