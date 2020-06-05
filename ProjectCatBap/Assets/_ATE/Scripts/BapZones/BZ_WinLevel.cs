using ATE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Baps.Zones
{
	public class BZ_WinLevel: BapZone
	{
        protected override void OnTriggered(Bappee bappee)
        {
            GS_Events.Invoke (EventID.WinLevel, LevelEnding.WinReasons.Zone);
        }

    }
}