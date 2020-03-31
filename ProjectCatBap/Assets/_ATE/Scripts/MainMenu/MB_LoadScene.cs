using ATE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE
{
	public class MB_LoadScene : MenuButton
	{
        public int sceneBuildIndex = 0;


        protected override void OnClicked()
        {
            GS_Events.Invoke (EventID.LoadLevel, sceneBuildIndex);
        }

    }
}