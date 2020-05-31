using ATE.Events;
using ATE.Points;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Feedback
{
	public class PointsUI : MonoBehaviour
	{
        public TextMesh pointsNumber;


        private void Start()
        {
            GS_Events.AddListener (EventID.PointsChanged, PointsChanged);
            PointsChanged (GS_Points.instance.Points);
        }

        private void PointsChanged(params object[] args)
        {
            pointsNumber.text = ((int)args[0]).ToString ();
        }

    }
}