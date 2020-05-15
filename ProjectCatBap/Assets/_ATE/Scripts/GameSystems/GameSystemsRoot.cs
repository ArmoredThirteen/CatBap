using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE
{
	public class GameSystemsRoot : MonoBehaviour
	{
        public static GameSystemsRoot instance = null;

		private void Awake()
        {
            // Singleton
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);

            DontDestroyOnLoad (this.gameObject);
        }
		
	}
}