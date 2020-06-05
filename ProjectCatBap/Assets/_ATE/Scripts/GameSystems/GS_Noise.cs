using ATE.Events;
using ATE.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Noise
{
	public class GS_Noise : GameSystem
	{
        [HideInInspector]
        public static GS_Noise instance = null;

        public bool loseLevelOnMaxNoise = true;
        public float noiseDecay = 1;
        public List<float> noiseLevels = new List<float> ();

        
        public float Noise
        {
            private set; get;
        }

        public int NoiseLevel
        {
            private set; get;
        }

        public bool IsMaxNoiseLevel
        {
            get
            {
                return NoiseLevel >= noiseLevels.Count;
            }
        }


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
            GS_Events.AddListener (EventID.AddNoise, AddNoise);
            GS_Events.AddListener (EventID.RemoveNoise, RemoveNoise);
            GS_Events.AddListener (EventID.SetNoise, SetNoise);

            Noise = 0;
            NoiseLevel = 0;
        }

        private void Update()
        {
            RemoveNoise (noiseDecay * Time.deltaTime);
        }


        public void AddNoise(params object[] args)
        {
            // Add noise
            Noise += (float)args[0];
            InvokeNoiseChanged ();

            // Increase noise level
            if (noiseLevels.Count == 0 || IsMaxNoiseLevel)
                return;
            if (Noise < noiseLevels[NoiseLevel])
                return;

            NoiseLevel++;
            InvokeNoiseLevelChanged ();
        }

        public void RemoveNoise(params object[] args)
        {
            if (Noise <= 0)
                return;

            // Remove noise
            Noise = Mathf.Max (0, Noise - (float)args[0]);
            InvokeNoiseChanged ();

            // Decrease noise level
            if (noiseLevels.Count == 0 || NoiseLevel <= 0)
                return;
            if (Noise >= noiseLevels[NoiseLevel - 1])
                return;

            NoiseLevel--;
            InvokeNoiseLevelChanged ();
        }

        public void SetNoise(params object[] args)
        {
            float newNoise = (float)args[0];

            if (newNoise > Noise)
                AddNoise (newNoise - Noise);
            else if (newNoise < Noise)
                RemoveNoise (Noise - newNoise);
        }


        // For other systems to receive info about new noise value.
        // If they were to respond to Add or RemovePoints instead they could trigger
        //   before this class triggers, and report an out of sync value.
        private void InvokeNoiseChanged()
        {
            //Debug.Log ("New noise: " + Noise);
            GS_Events.Invoke (EventID.NoiseChanged, Noise);
        }

        // For other systems to receive info about new noise levels.
        private void InvokeNoiseLevelChanged()
        {
            //Debug.Log ("New noise level: " + NoiseLevel);
            GS_Events.Invoke (EventID.NoiseLevelChanged, NoiseLevel);

            if (loseLevelOnMaxNoise && IsMaxNoiseLevel)
                GS_Events.Invoke (EventID.LoseLevel, LevelEnding.LoseReasons.MaxNoise);
        }

    }
}