using ATE.Events;
using ATE.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE
{
    // Calls Kongregate API functions (which are probably javascript)
    //TODO: Migrate to solutions found here:
    //      https://docs.kongregate.com/discuss/59aa698f93a131001b96a862
    //      https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html
	public class GS_KongAPI : GameSystem
	{
		[HideInInspector]
        public static GS_KongAPI instance = null;

        public bool loadGameOnAPILoaded = true;


        private bool isLoaded = false;

        private int userid = 0;
        private string username = "Guest";
        private string auth = "";


        public static int GetUserid ()
        {
            return instance.userid;
        }

        public static string GetUsername()
        {
            return instance.username;
        }

        public static string GetAuth()
        {
            return instance.auth;
        }


        private void Awake()
        {
            // Singleton
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);
        }

        private void Start() {
		    instance.Connect();
	    }


        private void Connect() {
            if (!isLoaded)
            {
                Application.ExternalEval ("if(typeof(kongregateUnitySupport) != 'undefined') {" +
                    "kongregateUnitySupport.initAPI('" + gameObject.name + "', 'OnKongregateAPILoaded');" +
                    "}");
            }
	    }

        void OnKongregateAPILoaded(string userInfo) {
            isLoaded = true;

		    string[] userStats = userInfo.Split("|"[0]);
		    userid = int.Parse(userStats[0]);
		    username = userStats[1];
		    auth = userStats[2];

            //Debug.Log ($"Kong -> userID: {userid}, userName: {username}, authToken: {auth}");

            // Make sure game is loaded, since load on startup usually happens before the API call is complete
            if (loadGameOnAPILoaded)
                GS_Events.Invoke (EventID.LoadGame, null);
	    }
		
	}
}