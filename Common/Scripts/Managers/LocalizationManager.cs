﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Common
{
    public class LocalizationManager : Manager<LocalizationManager>
    {
        private Dictionary<string, string> localizedText;
        private bool isReady = false;
        private string missingTextString = "Localized text not found";

        public void LoadLocalizedText(string fileName)
        {
            localizedText = new Dictionary<string, string>();
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

                for (int i = 0; i < loadedData.items.Length; i++)
                {
                    localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                }

                Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
            }
            else
            {
                Debug.LogError("Cannot find file!");
            }

            isReady = true;
        }

        public string GetLocalizedValue(string key)
        {
            string result = missingTextString;
            if (localizedText.ContainsKey(key))
            {
                result = localizedText[key];
            }

            return result;

        }

        public bool GetIsReady()
        {
            return isReady;
        }

    }
}

