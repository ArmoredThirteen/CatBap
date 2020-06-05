using ATE.Events;
using ATE.Levels;
using ATE.Menu;
using ATE.Points;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ATE.Levels.GS_LevelData;

namespace ATE.LevelEnding
{
	public class WinScene : MonoBehaviour
	{
        public TextMesh feedback;
        public MB_LoadScene buttonReplayLevel;
        public MB_LoadScene buttonNextLevel;


        private void Start()
        {
            Level lastLevel = GS_LevelEnder.instance.LastEndedLevel;
            Level nextLevel = GS_LevelData.GetNextLevel (lastLevel);
            int points = GS_Points.instance.Points;

            SetFeedback (points);
            ButtonSetup (lastLevel, nextLevel);
            ApplyLevelResults (lastLevel, nextLevel, points);
            ResetGameSystems ();
        }


        private void SetFeedback(int points)
        {
            feedback.text = $"Level won with score of: {points}";
        }

        private void ButtonSetup(Level lastLevel, Level nextLevel)
        {
            buttonReplayLevel.sceneName = lastLevel.sceneName;

            // If next level exists set up button, otherwise destroy it
            if (nextLevel != null)
                buttonNextLevel.sceneName = nextLevel.sceneName;
            else
                GameObject.Destroy (buttonNextLevel.gameObject);
        }

        private void ApplyLevelResults(Level lastLevel, Level nextLevel, int points)
        {
            if (points < lastLevel.highscore)
                return;

            // Set level's highscore and unlock next level
            lastLevel.highscore = points;
            if (nextLevel != null)
                nextLevel.locked = false;

            GS_Events.Invoke (EventID.SaveGame);
        }

        private void ResetGameSystems()
        {
            GS_Events.Invoke (EventID.SetPoints, 0);
            GS_Events.Invoke (EventID.SetNoise, 0f);
        }

    }
}