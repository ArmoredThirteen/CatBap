using ATE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Points
{
	public class PointsManager : MonoBehaviour
	{
        public static PointsManager instance = null;

        public int Points
        {
            private set; get;
        }


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);
        }

        private void Start()
        {
            EventManager.AddListener (EventID.AddPoints, AddPoints);
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
            EventManager.Invoke (EventID.PointsChanged, instance.Points);
        }

    }
}