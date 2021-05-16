using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AlphabetBook
{
    [CreateAssetMenu(fileName = "DragAndDrop", menuName = "Alphabet/DragAndDropGame")]
    public class DragAndDropDetails : ScriptableObject
    {
        [Serializable]
        public class Details
        {
            public List<Sprite> sprites;
            public List<string> tags;
            public int correctCount;
        }


        public List<Details> GetDetails;

    }
}

