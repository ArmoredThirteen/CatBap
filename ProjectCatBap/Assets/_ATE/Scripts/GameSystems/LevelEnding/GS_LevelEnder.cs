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
                    break;
            }
        }

        private void LoseLevel(object[] args)
        {
            LossReasons reason = (LossReasons)args[0];

            switch (reason)
            {
                case LossReasons.LoseZone:
                    break;
                case LossReasons.MaxNoise:
                    break;
            }
        }

    }
}