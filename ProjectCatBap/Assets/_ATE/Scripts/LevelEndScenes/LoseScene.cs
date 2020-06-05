using ATE.Events;
using ATE.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ATE.Levels.GS_LevelData;

namespace ATE.LevelEnding
{
	public class LoseScene : MonoBehaviour
	{
        public TextMesh feedback;
        public MB_LoadScene buttonRetryLevel;


		private void Start()
        {
            Level lastLevel = GS_LevelEnder.instance.LastEndedLevel;

            SetFeedback ();
            ButtonSetup (lastLevel);
            ResetGameSystems ();
        }


        private void SetFeedback()
        {
            feedback.text = "Level lost!";
        }

        private void ButtonSetup(Level lastLevel)
        {
            buttonRetryLevel.sceneName = lastLevel.sceneName;
        }

        private void ResetGameSystems()
        {
            GS_Events.Invoke (EventID.SetPoints, 0);
            GS_Events.Invoke (EventID.SetNoise, 0f);
        }
		
	}
}