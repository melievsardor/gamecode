using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class GameBase : MonoBehaviour, IItemDropHandler
    {
        [SerializeField]
        protected List<Image> dropItems = new List<Image>();

        [SerializeField]
        protected Canvas canvas = null;

        protected Camera gameCamera;

        protected Gaming gaming;

        protected int index;

        private Color backgroundColor = new Color(251f / 255f, 250f / 255f, 240f / 255f, 0f);

        protected virtual void Start()
        {
            gaming = FindObjectOfType<Gaming>();

            gameCamera = GameObject.FindGameObjectWithTag("GameCamera").GetComponent<Camera>();

            gaming.backgroundObject.SetActive(true);
            gameCamera.backgroundColor = backgroundColor;

            if (canvas != null)
                canvas.worldCamera = gameCamera;

        }

        public virtual void OnCompletedItem()
        {

        }

        protected virtual void NextItem()
        {
           
        }

        public virtual void OnReset()
        {
           
        }


    }
}


