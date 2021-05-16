using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlphabetBook
{
    [CreateAssetMenu(fileName = "FindGame", menuName = "Alphabet/FindGame")]
    public class FindGameDetails : ScriptableObject
    {
        [Serializable]
        public class Questions
        {
            public Sprite letter;

            public Sprite answer;

            public Sprite question0;
            public Sprite question1;
            public Sprite question2;
        }

        [SerializeField]
        public List<Questions> questions;

    }
}


