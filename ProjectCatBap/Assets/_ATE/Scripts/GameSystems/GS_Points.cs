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
            GS_Events.AddListener (EventID.AddPoints, AddPoints);
            GS_Events.AddListener (EventID.RemovePoints, AddPoints);
            GS_Events.AddListener (EventID.SetPoints, SetPoints);
        }


        public void AddPoints(object[] args)
        {
            Points += (int)args[0];
            InvokePointsChanged ();
        }

        public void RemovePoints(object[] args)
        {
            Points -= (int)args[0];
            InvokePointsChanged ();
        }

        public void SetPoints(object[] args)
        {
            Points = (int)args[0];
            InvokePointsChanged ();
        }


        // For other systems to receive info about new points value.
        // If they were to respond to Add or SetPoints instead they could trigger
        //   before this class triggers, and report an out of sync value.
        private void InvokePointsChanged()
        {
            //Debug.Log ("New points: " + Points);
            GS_Events.Invoke (EventID.PointsChanged, Points);
        }

    }
}