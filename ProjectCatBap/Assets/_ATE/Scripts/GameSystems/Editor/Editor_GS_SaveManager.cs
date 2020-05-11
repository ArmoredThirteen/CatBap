using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace ATE.GameSaves
{
    [CustomEditor(typeof(GS_SaveManager))]
	public class Editor_GS_SaveManager : Editor
	{
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI ();
            GS_SaveManager targ = (GS_SaveManager)target;

            if (GUILayout.Button ("Load Game"))
                targ.LoadGame (null);
            /*if (GUILayout.Button ("Save Game"))
                targ.SaveGame (null);*/
        }

        

    }
}