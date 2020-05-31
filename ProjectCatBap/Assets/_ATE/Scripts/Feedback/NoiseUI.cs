using ATE.Events;
using ATE.Noise;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Feedback
{
	public class NoiseUI : MonoBehaviour
	{
        public TextMesh noiseNumber;

        public Color minNoiseLevelColor;
        public Color maxNoiseLevelColor;
        public List<Color> noiseLevelColors;


		private void Start()
        {
            GS_Events.AddListener (EventID.NoiseChanged, NoiseChanged);
            GS_Events.AddListener (EventID.NoiseLevelChanged, NoiseLevelChanged);

            NoiseChanged (GS_Noise.instance.Noise);
            NoiseLevelChanged (GS_Noise.instance.NoiseLevel);
        }


        private void NoiseChanged(params object[] args)
        {
            noiseNumber.text = ((float)args[0]).ToString ("0.0");
        }

        private void NoiseLevelChanged(params object[] args)
        {
            int colorIndex = (int)args[0];

            if (colorIndex < 0)
            {
                noiseNumber.color = minNoiseLevelColor;
                return;
            }
            if (colorIndex >= noiseLevelColors.Count)
            {
                noiseNumber.color = maxNoiseLevelColor;
                return;
            }

            noiseNumber.color = noiseLevelColors[colorIndex];
        }
		
	}
}