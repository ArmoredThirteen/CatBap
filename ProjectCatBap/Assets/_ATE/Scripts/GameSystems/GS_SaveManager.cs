#define DEBUGLOG

using ATE.Events;
using ATE.GameSystems;
using ATE.Scenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.GameSaves
{
	public class GS_SaveManager : GameSystem
	{
		[HideInInspector]
        public static GS_SaveManager instance = null;

        public string prefix = "ATE_CatBap_";
        public string key_unlockedLevels = "UnlockedLevels";


        private string UnlockedLevels { get { return prefix + key_unlockedLevels; } }


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
            GS_Events.AddListener (EventID.LoadGame, LoadGame);
            GS_Events.AddListener (EventID.SaveGame, SaveGame);
            GS_Events.AddListener (EventID.WipeGame, WipeGame);
        }


        public void LoadGame(object[] args)
        {
            #if DEBUGLOG
            Debug.Log ("Loading Game");
            #endif

            GS_LevelManager.instance.unlockedLevels = PlayerPrefs.GetInt (UnlockedLevels);
        }

        public void SaveGame(object[] args)
        {
            #if DEBUGLOG
            Debug.Log ("Saving Game");
            #endif

            PlayerPrefs.SetInt (UnlockedLevels, GS_LevelManager.instance.unlockedLevels);
            PlayerPrefs.Save ();
        }

        public void WipeGame(object[] args)
        {
            #if DEBUGLOG
            Debug.Log ("Wiping Game");
            #endif

            if (PlayerPrefs.HasKey (UnlockedLevels))
                PlayerPrefs.DeleteKey (UnlockedLevels);
        }
		
	}
}