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


        private bool isLoaded = false;

        private int userID = 0;
        private string userName = "Guest";
        private string gameAuthToken = "";


        private void Awake()
        {
            // Singleton
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);
        }


        void Start() {
		    instance.Connect();
	    }

	    void Connect() {
            if (!isLoaded)
            {
                Debug.Log ("Not isloaded");
                Application.ExternalEval ("if(typeof(kongregateUnitySupport) != 'undefined') {" +
                    "kongregateUnitySupport.initAPI('" + gameObject.name + "', 'OnKongregateAPILoaded');" +
                    "}");
            }
            else
            {
                Debug.Log ("Is isloaded");
            }
	    }

        void OnKongregateAPILoaded(string userInfo) {
		    isLoaded = true;

		    string[] userStats = userInfo.Split("|"[0]);
		    userID = int.Parse(userStats[0]);
		    userName = userStats[1];
		    gameAuthToken = userStats[2];

            Debug.Log ($"Kong -> userID: {userID}, userName: {userName}, authToken: {gameAuthToken}");
	    }
		
	}
}