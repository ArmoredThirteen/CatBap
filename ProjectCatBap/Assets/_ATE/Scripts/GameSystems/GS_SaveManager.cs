#define DEBUGLOG

using ATE.Events;
using ATE.GameSystems;
using ATE.Scenes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ATE.GameSaves
{
	public class GS_SaveManager : GameSystem
	{
        [System.Serializable]
        public class JsonSaveObj
        {
            [System.Serializable]
            public class Level
            {
                public int score;

                public Level(int theScore)
                {
                    score = theScore;
                }
            }

            public Level[] levels;

            public JsonSaveObj(int[] levelScores)
            {
                levels = new Level[levelScores.Length];
                for (int i = 0; i < levelScores.Length; i++)
                    levels[i] = new Level (levelScores[i]);
            }

            public List<int> GetScores()
            {
                List<int> levelScores = new List<int> ();
                for (int i = 0; i < levels.Length; i++)
                    levelScores.Add (levels[i].score);
                return levelScores;
            }
        }


		[HideInInspector]
        public static GS_SaveManager instance = null;


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
            string uri = $"http://armoredthirteen.net/ws_GameSaver.php?game={GetGame()}&userid={GetUserid()}&username={GetUsername()}";
            StartCoroutine (WebLoadGame (uri));
        }

        public void SaveGame(object[] args)
        {
            //StartCoroutine (WebSaveGame ());
        }

        public void WipeGame(object[] args)
        {

        }


        private IEnumerator WebLoadGame(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get (uri))
            {
                yield return webRequest.SendWebRequest ();

                if (webRequest.isNetworkError)
                    Debug.Log ($"Error: {webRequest.error}");
                else if (webRequest.responseCode != 200)
                    Debug.Log ($"Invalid response code {webRequest.responseCode}: {webRequest.downloadHandler.text}");
                else
                {
                    Debug.Log (webRequest.downloadHandler.text);
                    JsonSaveObj responseObj = JsonUtility.FromJson<JsonSaveObj> (webRequest.downloadHandler.text);
                    GS_LevelManager.instance.levelScores = responseObj.GetScores ();
                }
            }
        }

        /*private IEnumerator WebSaveGame(string uri)
        {

        }*/


        private string GetGame()
        {
            return "CatBap";
        }

        private string GetUserid()
        {
            return "666";
        }

        private string GetUsername()
        {
            return "ArmoredThirteen";
        }

        private string GetSaveJson()
        {
            return JsonUtility.ToJson (new JsonSaveObj (GS_LevelManager.instance.levelScores.ToArray ()));
        }
		
	}
}