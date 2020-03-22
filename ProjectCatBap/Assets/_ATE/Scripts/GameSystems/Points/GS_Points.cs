using ATE.Events;
using ATE.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Points
{
	public class GS_Points : GameSystem
	{
        [HideInInspector]
        public static GS_Points instance = null;

        public int Points
        {
            private set; get;
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
            //TODO: May behave weirdly during scene change, not sure until tested
            GS_Events.AddListener (EventID.AddPoints, AddPoints);
            GS_Events.AddListener (EventID.SetPoints, SetPoints);
        }


        public void AddPoints(object[] args)
        {
            instance.Points += (int)args[0];
            InvokePointsChanged ();
        }

        public void SetPoints(object[] args)
        {
            instance.Points = (int)args[0];
            InvokePointsChanged ();
        }

        // For other systems to receive info about new points value.
        // If they were to respond to Add or SetPoints instead they could trigger
        //   before this class triggers, and report an out of sync value.
        public void InvokePointsChanged()
        {
            Debug.Log ("New points: " + instance.Points);
            GS_Events.Invoke (EventID.PointsChanged, instance.Points);
        }

    }
}