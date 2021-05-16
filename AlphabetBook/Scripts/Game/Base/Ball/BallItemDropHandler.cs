using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AlphabetBook
{
    public class BallItemDropHandler : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private Transform parent = null;

        private BallBase ballBase;

        private void Start()
        {
            ballBase = GetComponentInParent<BallBase>();
        }

        public void OnDrop(PointerEventData eventData)
        {
           // string temp = "Item" + ballBase.CurrentIndex;

            if (BallItemDragHandler.itemBeingDrag.tag == this.tag)
            {
                BallItemDragHandler.itemBeingDrag.LetterTransform.SetParent(parent);

                ballBase.OnCompletedItem();
            }
        }// end
    }
}


