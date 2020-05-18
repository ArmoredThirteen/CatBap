using ATE.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE
{
	public class GS_LogDisplay : GameSystem
	{
        public int maxLogs = 5;
        public TextMesh text;
        public Vector3 textPosition;

        private Queue<string> logs = new Queue<string> ();


        private void Awake()
        {
            text.transform.position = textPosition;
        }


        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (logs.Count > maxLogs)
                logs.Dequeue ();
            logs.Enqueue ($"{type}: {logString}");

            string toDisplay = "";
            foreach (string str in logs)
                toDisplay = $"{toDisplay}\r\n{str}";

            text.text = toDisplay;
        }
		
	}
}