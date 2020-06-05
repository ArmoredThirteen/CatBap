using ATE.Events;
using ATE.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.LevelEnding
{
	public class GS_LevelEnder : GameSystem
	{
		[HideInInspector]
        public static GS_LevelEnder instance = null;


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
            WinReasons reason = (WinReasons)args[0];

            switch (reason)
            {
                case WinReasons.WinZone:
                    WinLevel ();
                    break;
            }
        }

        private void LoseLevel(object[] args)
        {
            LossReasons reason = (LossReasons)args[0];

            switch (reason)
            {
                case LossReasons.LoseZone:
                    LoadLevelSelect ();
                    break;
                case LossReasons.MaxNoise:
                    LoadLevelSelect ();
                    break;
            }
        }


        private void WinLevel()
        {
            //TODO: Store the next level and go to 'continue to next level screen'
            //TODO: Store highscore, unlock next level, save game, reset current points and noise
            GS_Events.Invoke (EventID.LoadLevel, "Level Select");
        }

        private void LoadLevelSelect()
        {
            //TODO: Go to 'retry level' screen
            GS_Events.Invoke (EventID.LoadLevel, "Level Select");
        }

    }
}