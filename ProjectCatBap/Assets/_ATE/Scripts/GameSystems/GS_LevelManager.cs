using ATE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ATE.Scenes
{
	public class GS_LevelManager : MonoBehaviour
	{
		[HideInInspector]
        public static GS_LevelManager instance = null;

        public List<int> levelScores = new List<int> () { 0 };


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
            //GS_Events.AddListener (EventID.WinLevel, WinLevel);
            //GS_Events.AddListener (EventID.LoseLevel, LoseLevel);

            // Make sure at least the first level is unlocked
            //TODO: Might be better to just exclude the tutorial level from the scoring/locking system
            if (levelScores == null || levelScores.Count <= 0)
                levelScores = new List<int> () { 0 };
        }


        public void LoadLevel(object[] args)
        {
            string sceneName = (string)args[0];
            Debug.Log ($"Loading Scene [{sceneName}]");
            SceneManager.LoadSceneAsync (sceneName);
        }

	}
}