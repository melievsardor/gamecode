
using UnityEngine;

namespace AlphabetBook
{
    [CreateAssetMenu(fileName = "AlphabetStats", menuName = "Alphabet/Stats")]
    public class AlphabetStats : ScriptableObject
    {
        [SerializeField]
        private int level = 0;

        [SerializeField]
        private int buttonCount = 33;

        [SerializeField]
        private int currentButton = 1;

        [SerializeField]
        private int oldButton = 0;

        private int cupCount;
        public int CupCount { get { return cupCount; } set { cupCount = value; } }

        [SerializeField]
        private string key = "RuAlphabetStats";

        [SerializeField]
        private int[] levelIndex = { 8, 20, 27, 33 };
        public int[] LevelIndex { get { return levelIndex; } }

        [SerializeField]
        private bool isFinish = false;
        public bool IsFinish { get { return isFinish; } set { isFinish = value; } }

        public int Level { get { return level; } set { level = value; } }

        public int ButtonCount { get { return buttonCount; } }

        public int CurrentButton { get { return currentButton; } set { currentButton = value; } }

        public int OldButton { get { return oldButton; } set { oldButton = value; } }


        private bool isOpenNewLetter;
        public bool IsOpenNewLetter { get { return isOpenNewLetter; } }


        [SerializeField]
        private bool isRU;
        public bool IsRU { get { return isRU; } }

        public void NextButton()
        {
            if(oldButton == currentButton - 1)
            {
                currentButton = Mathf.Clamp(currentButton + 1, 0, buttonCount);
                isOpenNewLetter = true;
            }
            else
            {
                isOpenNewLetter = false;
            }

            NextLevel(OldButton);


        }


        private void NextLevel(int index)
        {
            if (index >= 0 && index < levelIndex[0])
            {
                level = 0;
            }
            else if (index >= levelIndex[0] && index < levelIndex[1])
            {
                level = 1;
            }
            else if (index >= levelIndex[1] && index < levelIndex[2])
            {
                level = 2;
            }
            else if (index >= levelIndex[2] && index < levelIndex[3])
            {
                level = 3;
            }

            SaveData();
        }


        private void OnEnable()
        {
            LoadData();
        }


        private void OnDisable()
        {
            SaveData();
        }


        public void LoadData()
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);
        }

        private void SaveData()
        {
            if (key == string.Empty)
            {
                key = name;
            }

            string jsonData = JsonUtility.ToJson(this, true);
            PlayerPrefs.SetString(key, jsonData);
            PlayerPrefs.Save();
        }

    }
}


