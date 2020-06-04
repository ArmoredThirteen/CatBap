using ATE.Events;
using ATE.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ATE.Levels
{
	public class GS_LevelLoader : GameSystem
	{
		[HideInInspector]
        public static GS_LevelLoader instance = null;


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
            GS_Events.AddListener (EventID.LoadLevel, LoadLevel);
        }


        public void LoadLevel(object[] args)
        {
            string sceneName = (string)args[0];
            //Debug.Log ($"Loading Scene [{sceneName}]");
            SceneManager.LoadSceneAsync (sceneName);
        }

	}
}