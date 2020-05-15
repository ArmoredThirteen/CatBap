using ATE.Events;
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

        public int columns = 3;
        public float columnSpacing = 4;
        public float rowSpacing = 2;


        private void Start()
        {
            GS_Events.AddListener (EventID.GameLoaded, RebuildButtons);
            RebuildButtons (null);
        }


        private void RebuildButtons(object[] args)
        {
            // Destroy the child buttons
            foreach (Transform child in transform)
                GameObject.Destroy(child.gameObject);

            // Build one button for each level's data
            GS_LevelData.Level[] levelDatas = GS_LevelData.instance.levels;
            for (int i = 0; i < levelDatas.Length; i++)
            {
                float xPos = (i % columns) * columnSpacing;
                float yPos = (i / columns) * -rowSpacing;

                // Button is locked or actually goes to a scene
                if (levelDatas[i].locked)
                    BuildButton_Locked (xPos, yPos);
                else
                    BuildButton_Level (levelDatas[i].sceneName, levelDatas[i].highscore, xPos, yPos);
            }
        }
        

        private void BuildButton_Locked(float xPos, float yPos)
        {
            GameObject button = GameObject.Instantiate (lockedPrefab, transform);
            button.transform.localPosition = new Vector3 (xPos, yPos, 0);
        }

        private void BuildButton_Level(string sceneName, int highscore, float xPos, float yPos)
        {
            MB_LoadScene button = GameObject.Instantiate<MB_LoadScene> (buttonPrefab, transform);
            button.transform.localPosition = new Vector3 (xPos, yPos, 0);
            button.sceneName = sceneName;
            button.labelText.text = sceneName;
            button.secondaryText.text = "Highscore: " + highscore;
        }

    }
}