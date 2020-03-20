using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ATE.Events
{
    public class ThisEvent : UnityEvent<object[]> {}
    
	public class EventManager : MonoBehaviour
	{
        [HideInInspector]
        public static EventManager instance = null;

        private Dictionary<EventID, ThisEvent> events = null;


        private void Awake()
        {
            // Singleton
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);

            if (events == null)
                events = new Dictionary<EventID, ThisEvent> ();
        }


        public static void AddListener(EventID eventID, UnityAction<object[]> listener)
        {
            if (instance.events.ContainsKey (eventID))
                instance.events[eventID].AddListener (listener);
            else
            {
                ThisEvent newEvent = new ThisEvent ();
                newEvent.AddListener (listener);
                instance.events.Add (eventID, newEvent);
            }
        }

        public static void RemoveListener(EventID eventID, UnityAction<object[]> listener)
        {
            if (instance.events.ContainsKey (eventID))
                instance.events[eventID].RemoveListener (listener);
        }

        public static void Invoke(EventID eventID, params object[] args)
        {
            if (instance.events.ContainsKey (eventID))
                instance.events[eventID].Invoke (args);
        }

    }
}