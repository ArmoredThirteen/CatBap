using ATE.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Levels
{
	public class GS_LevelData : GameSystem
	{
        [System.Serializable]
        public class Data
        {
            public string sceneName = "";
            public bool unlocked = false;
            public int highscore = 0;
        }

        [HideInInspector]
        public static GS_LevelData instance = null;

        public Data[] levelDatas;


        private void Awake()
        {
            // Singleton
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);
        }
		
	}
}