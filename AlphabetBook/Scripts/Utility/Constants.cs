using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphabetBook
{
    public static class Constants
    {

        public static int currentLetter;

        public static int tempLevelIndex;

        public static Color unselectColor = new Color(151f / 255f, 132f / 255f, 131f / 255f, 255f / 255f);
        public static Color selectColor = new Color(236f / 255f, 167f / 255f, 91f / 255f, 255f / 255f);


        private static int itemIndex;
        public static int GetItemIndex { get { return itemIndex++; } set { itemIndex = value; } }

        private static int newItemIndex;
        public static int GetNewItemIndex { get { return newItemIndex++; } set { newItemIndex = value; } }

        public static int[] GetRandomIndex(int count)
        {
            List<int> indexs = new List<int>();
            int random = Random.Range(0, count);

            indexs.Add(random);

            while (indexs.Count < count)
            {
                random = Random.Range(0, count);
                if (!indexs.Contains(random))
                {
                    indexs.Add(random);
                }
            }

            return indexs.ToArray();
        }
    }

}
