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
        public float currNoise = 0;

        public float noiseDecay = 1;

        public List<float> noiseLevels = new List<float> ();


        private int currNoiseLevel = 0;


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
            GS_Events.AddListener (EventID.RemoveNoise, RemoveNoise);
            //GS_Events.AddListener (EventID.NoiseLevelIncrease, LogNoiseLevelIncrease);
            //GS_Events.AddListener (EventID.NoiseLevelDecrease, LogNoiseLevelDecrease);
        }

        private void Update()
        {
            GS_Events.Invoke (EventID.RemoveNoise, noiseDecay * Time.deltaTime);
        }


        public void AddNoise(object[] args)
        {
            // Add noise and reset decay delay
            currNoise += (float)args[0];

            // Increase noise level
            if (noiseLevels.Count == 0 || currNoiseLevel >= noiseLevels.Count)
                return;
            if (currNoise < noiseLevels[currNoiseLevel])
                return;

            currNoiseLevel++;
            GS_Events.Invoke (EventID.NoiseLevelIncrease, currNoiseLevel, currNoise);
        }

        public void RemoveNoise(object[] args)
        {
            // Remove noise
            currNoise = Mathf.Max (0, currNoise - (float)args[0]);

            if (noiseLevels.Count == 0 || currNoiseLevel <= 0)
                return;
            if (currNoise >= noiseLevels[currNoiseLevel - 1])
                return;

            currNoiseLevel--;
            GS_Events.Invoke (EventID.NoiseLevelDecrease, currNoiseLevel, currNoise);
        }

        public void LogNoiseLevelIncrease(object[] args)
        {
            Debug.Log ("Noise level INcreased: " + args[0] + ", current noise is: " + args[1]);
        }

        public void LogNoiseLevelDecrease(object[] args)
        {
            Debug.Log ("Noise level DEcreased: " + args[0] + ", current noise is: " + args[1]);
        }

    }
}