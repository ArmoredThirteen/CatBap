using ATE.Events;
using ATE.GameSystems;
using ATE.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ATE.Levels.GS_LevelData;

namespace ATE.LevelEnding
{
	public class GS_LevelEnder : GameSystem
	{
		[HideInInspector]
        public static GS_LevelEnder instance = null;

        //TODO: Seems a bit hacky but currently best spot to store level for win/loss scenes
        public Level LastEndedLevel
        {
            private set; get;
        }

        public WinReasons LastWinReason
        {
            private set; get;
        }

        public LoseReasons LastLoseReason
        {
            private set; get;
        }


        private void Awake()
        {
            // Singleton
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);
        }

        private void Start()
        {
            GS_Events.AddListener (EventID.WinLevel, WinLevel);
            GS_Events.AddListener (EventID.LoseLevel, LoseLevel);
        }


        private void WinLevel(object[] args)
        {
            LastWinReason = (WinReasons)args[0];
            LastEndedLevel = GS_LevelData.GetActiveLevel ();

            switch (LastWinReason)
            {
                case WinReasons.Zone:
                    Win_Zone ();
                    break;
            }
        }

        private void LoseLevel(object[] args)
        {
            LastLoseReason = (LoseReasons)args[0];
            LastEndedLevel = GS_LevelData.GetActiveLevel ();

            switch (LastLoseReason)
            {
                case LoseReasons.Zone:
                    Lose_Zone ();
                    break;
                case LoseReasons.MaxNoise:
                    Lose_MaxNoise ();
                    break;
            }
        }


        private void Win_Zone()
        {
            //TODO: Store highscore, unlock next level, save game, reset current points and noise
            GS_Events.Invoke (EventID.LoadLevel, "Level Won");
        }


        private void Lose_Zone()
        {
            GS_Events.Invoke (EventID.LoadLevel, "Level Lost");
        }

        private void Lose_MaxNoise()
        {
            GS_Events.Invoke (EventID.LoadLevel, "Level Lost");
        }

    }
}