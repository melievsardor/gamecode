using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace AlphabetBook
{
    public class ImageDragHandler : ItemDragHandlerBase
    {

        public Vector2 startScale = Vector2.one;

        public Vector2 endScale = Vector2.one;

        private Image image;


        protected override void Start()
        {
            base.Start();

            image = GetComponent<Image>();
        }

        protected override void OnBeginDrag()
        {
            base.OnBeginDrag();

            image.raycastTarget = isActive;
        }


        protected override void OnDrag()
        {
            base.OnDrag();

            image.transform.DOScale(endScale, 0.1f).SetEase(Ease.OutElastic);
        }


        protected override void OnEndDrag()
        {
            base.OnEndDrag();

            image.raycastTarget = isActive;

            if (!isActive)
            {
                image.transform.localScale = endScale;
                image.transform.localEulerAngles = Vector3.zero;
                image.rectTransform.anchoredPosition = Vector2.zero;
            }
            else
            {
                image.transform.DOScale(startScale, 0.2f).SetEase(Ease.InOutElastic);
            }


        }


    }
}


