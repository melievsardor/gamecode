using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphabetBook
{
    [CreateAssetMenu(fileName = "TozzleGame", menuName = "Alphabet/TozzleGame")]
    public class TozzleGameDetails : ScriptableObject
    {
        [Serializable]
        public class Detail
        {
            public List<Sprite> itemsSprite;
        }

        [SerializeField]
        public List<Detail> details = new List<Detail>();
    }
}


