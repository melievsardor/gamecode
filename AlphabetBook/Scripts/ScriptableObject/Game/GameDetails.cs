using System;
using UnityEngine;


namespace AlphabetBook
{
    [CreateAssetMenu(fileName = "GameDetails", menuName = "Alphabet/GameDetails")]
    public class GameDetails : ScriptableObject
    {
        [Serializable]
        public class Detail
        {
            public Sprite sprite;
        }

        [SerializeField]
        public Detail[] details;
    }

    
}


