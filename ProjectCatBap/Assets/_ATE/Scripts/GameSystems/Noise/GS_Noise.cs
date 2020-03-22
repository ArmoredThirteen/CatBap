using ATE.Events;
using ATE.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.noise
{
	public class GS_Noise : GameSystem
	{
        [HideInInspector]
        public GS_Noise instance = null;

        //TODO: Not public
        public float currentNoise = 0;
        public float noiseDecay = 1;


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
            //TODO: May behave weirdly during scene change, not sure until tested
            GS_Events.AddListener (EventID.AddNoise, AddNoise);
        }

        private void Update()
        {
            // Reduce noise, to minimum of 0
            currentNoise = Mathf.Max (0, currentNoise - (noiseDecay * Time.deltaTime));

            if (currentNoise > 0)
                Debug.Log ("Noise: " + currentNoise);
        }


        public void AddNoise(object[] args)
        {
            currentNoise += (float)args[0];
        }

    }
}