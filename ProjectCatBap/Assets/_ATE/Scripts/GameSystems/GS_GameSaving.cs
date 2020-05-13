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

            public List<Level> levels = new List<Level> ();
        }


		[HideInInspector]
        public static GS_GameSaving instance = null;

        public bool loadOnStart = true;


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

            if (loadOnStart)
                LoadGame (null);
        }


        public void LoadGame(object[] args)
        {
            StartCoroutine (WebLoadGame (GetURI ()));
        }

        public void SaveGame(object[] args)
        {
            StartCoroutine (WebSaveGame (GetURI ()));
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
            UnityWebRequest webRequest = UnityWebRequest.Get (uri);

            yield return webRequest.SendWebRequest ();

            if (webRequest.isNetworkError)
                Debug.Log ($"Error: {webRequest.error}");
            else if (webRequest.responseCode != 200)
                Debug.Log ($"Invalid response code {webRequest.responseCode}: {webRequest.downloadHandler.text}");
            else
            {
                ApplySaveJson (webRequest.downloadHandler.text);
                //Debug.Log ("Loading: " + webRequest.downloadHandler.text);
            }
        }

        private IEnumerator WebSaveGame(string uri)
        {
            UnityWebRequest webRequest = new UnityWebRequest (uri, "POST");

            byte[] jsonToSend = new System.Text.UTF8Encoding ().GetBytes (BuildSaveJson ());
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw (jsonToSend);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer ();
            webRequest.SetRequestHeader ("Content-Type", "application/json");

            yield return webRequest.SendWebRequest ();

            if (webRequest.isNetworkError)
                Debug.Log ($"Error: {webRequest.error}");
            /*else
                Debug.Log ("Game saved");*/
        }


        private void ApplySaveJson(string json)
        {
            JsonSaveObj saveObj = JsonUtility.FromJson<JsonSaveObj> (json);

            for (int i = 0; i < saveObj.levels.Count; i++)
            {
                GS_LevelData.Level currLevelData = GS_LevelData.instance.levels[i];
                currLevelData.locked = false;
                currLevelData.highscore = saveObj.levels[i].score;
            }
        }

        private string BuildSaveJson()
        {
            GS_LevelData.Level[] levelDatas = GS_LevelData.instance.levels;
            JsonSaveObj saveObj = new JsonSaveObj ();

            for (int i = 0; i < levelDatas.Length; i++)
            {
                if (levelDatas[i].locked)
                    break;
                saveObj.levels.Add (new JsonSaveObj.Level (levelDatas[i].highscore));
            }

            return JsonUtility.ToJson (saveObj);
        }
		
	}
}