#define DEBUGLOG

using ATE.Events;
using ATE.GameSystems;
using ATE.Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ATE.GameSaves
{
	public class GS_GameSaving : GameSystem
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

            public List<Level> levels;
        }


		[HideInInspector]
        public static GS_GameSaving instance = null;


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
            StartCoroutine (WebLoadGame (GetURI ()));
        }

        public void SaveGame(object[] args)
        {
            //StartCoroutine (WebSaveGame (GetURI ()));
        }

        public void WipeGame(object[] args)
        {

        }

        private string GetURI()
        {
            string game = "CatBap";
            string userid = "666";
            string username = "ArmoredThirteen";

            return $"http://armoredthirteen.net/ws_GameSaver.php?game={game}&userid={userid}&username={username}";
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
                    ApplySaveJson (webRequest.downloadHandler.text);
            }
        }

        /*private IEnumerator WebSaveGame(string uri)
        {

        }*/


        private void ApplySaveJson(string json)
        {
            Debug.Log (json);
            JsonSaveObj saveObj = JsonUtility.FromJson<JsonSaveObj> (json);

            for (int i = 0; i < saveObj.levels.Count; i++)
            {
                GS_LevelData.Data currLevelData = GS_LevelData.instance.levelDatas[i];
                currLevelData.unlocked = true;
                currLevelData.highscore = saveObj.levels[i].score;
            }
        }

        private string BuildSaveJson()
        {
            GS_LevelData.Data[] levelDatas = GS_LevelData.instance.levelDatas;
            JsonSaveObj saveObj = new JsonSaveObj ();

            for (int i = 0; i < levelDatas.Length; i++)
            {
                if (!levelDatas[i].unlocked)
                    break;
                saveObj.levels.Add (new JsonSaveObj.Level (levelDatas[i].highscore));
            }

            return JsonUtility.ToJson (saveObj);
        }
		
	}
}