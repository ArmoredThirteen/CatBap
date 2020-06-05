using ATE.GameSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Levels
{
	public class GS_LevelData : GameSystem
	{
        [System.Serializable]
        public class Level
        {
            public string sceneName = "";
            public bool locked = true;
            public int highscore = 0;
        }

        [HideInInspector]
        public static GS_LevelData instance = null;

        public Level[] levels;


        private void Awake()
        {
            // Singleton
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);
        }


        public Level GetLevel(string levelName)
        {
            return Array.Find (levels, lvl => String.Equals (lvl, levelName));
        }

        // Return the level after the one found at levelName
        public Level GetNextLevel(string levelName)
        {
            for (int i = 0; i < levels.Length - 1; i++)
                if (String.Equals (levels[i].sceneName, levelName))
                    return levels[i + 1];

            return null;
        }
		
	}
}