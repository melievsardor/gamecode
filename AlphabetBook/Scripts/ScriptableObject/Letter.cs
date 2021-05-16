using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphabetBook
{
    [CreateAssetMenu(fileName = "new Letter", menuName = "Letter")]
    public class Letter : ScriptableObject
    {
        public string animName;
        public string letterName;
        public Sprite item0;
        public Sprite item1;

        public GameObject letterPrefab;

        public GameObject gamePrefab;

        public bool isLock = true;
    }

}

