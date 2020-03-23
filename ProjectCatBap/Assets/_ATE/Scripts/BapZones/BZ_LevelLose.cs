using ATE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Baps
{
	public class BZ_LevelLose: BapZone
	{
        protected override void OnTriggered(Bappee bappee)
        {
            GS_Events.Invoke (EventID.LevelLose);
        }

    }
}