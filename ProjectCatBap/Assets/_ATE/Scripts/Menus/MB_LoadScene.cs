using ATE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Menu
{
	public class MB_LoadScene : MenuButton
	{
        public string sceneName = "MainMenu";
        public TextMesh labelText;
        public TextMesh secondaryText;


        protected override void OnClicked()
        {
            GS_Events.Invoke (EventID.LoadLevel, sceneName);
        }

    }
}