using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AlphabetBook
{
    public class GroupDragHandler : ItemDragHandlerBase
    {

        [SerializeField]
        private Vector2 beganSize = Vector2.one;

        [SerializeField]
        private Vector2 endSize = Vector2.one;

        public GameObject parentObject;

        private RectTransform rectTransform;

        private GroupBase groupBase;

        protected override void Start()
        {
            base.Start();

            groupBase = FindObjectOfType<GroupBase>();

            rectTransform = GetComponent<RectTransform>();
        }

        protected override void OnBeginDrag()
        {
            base.OnBeginDrag();

            rectTransform.DOSizeDelta(new Vector2(endSize.x, endSize.y), 1f);

        }

        protected override void OnDrag()
        {
            base.OnDrag();
        }

        protected override void OnEndDrag()
        {
            base.OnEndDrag();

            if (!isActive)
            {
                parentObject.SetActive(false);

                gameObject.SetActive(false);
                //rectTransform.SetWidth(endSize.x);
                //rectTransform.SetHeight(endSize.y);

                //rectTransform.anchoredPosition = Vector2.zero;

            }
            else
            {
                rectTransform.DOSizeDelta(new Vector2(beganSize.x, beganSize.y), 1f);
                //rectTransform.anchoredPosition = Vector2.zero;
            }


            groupBase.ResetImages();
        }

    }
}


