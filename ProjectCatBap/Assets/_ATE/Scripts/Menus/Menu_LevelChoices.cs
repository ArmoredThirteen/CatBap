using ATE.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Menu
{
	public class Menu_LevelChoices : MonoBehaviour
	{
        public MB_LoadScene buttonPrefab;
        public GameObject lockedPrefab;

        public int columns = 4;
        public float columnSpacing = 4;
        public float rowSpacing = 2;


        private void Start()
        {
            GS_LevelData.Level[] levelDatas = GS_LevelData.instance.levels;

            for (int i = 0; i < levelDatas.Length; i++)
            {
                float xPos = i * columnSpacing;
                float yPos = 0;

                if (levelDatas[i].locked)
                    BuildButton_Locked (xPos, yPos);
                else
                    BuildButton_Level (levelDatas[i].sceneName, xPos, yPos);
            }
        }
        

        private void BuildButton_Locked(float xPos, float yPos)
        {
            GameObject button = GameObject.Instantiate (lockedPrefab, transform);
            button.transform.localPosition = new Vector3 (xPos, yPos, 0);
        }

        private void BuildButton_Level(string sceneName, float xPos, float yPos)
        {
            MB_LoadScene button = GameObject.Instantiate<MB_LoadScene> (buttonPrefab, transform);
            button.transform.localPosition = new Vector3 (xPos, yPos, 0);
            button.sceneName = sceneName;

            TextMesh text = button.GetComponentInChildren<TextMesh> ();
            text.text = sceneName;
        }

    }
}