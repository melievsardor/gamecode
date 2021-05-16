using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AlphabetBook
{
    [CreateAssetMenu(fileName = "About", menuName = "Alphabet/About")]
    public class AboutDetails : ScriptableObject
    {
        [Serializable]
        public class Details
        {
            public string letter;
            public AudioClip letterClip;

            public Sprite sprite0;
            public Vector2 size0;
            public AudioClip clip0;
            public Vector2 buttonPos0;

            public Sprite sprite1;
            public Vector2 size1;
            public AudioClip clip1;
            public Vector2 buttonPos1;

            public Sprite sprite2;
            public Vector2 size2;
            public AudioClip clip2;
            public Vector2 buttonPos2;

            public Sprite sprite3;
            public Vector2 size3;
            public AudioClip clip3;
            public Vector2 buttonPos3;

        }

        public List<Details> GetDetails;
    }
}


