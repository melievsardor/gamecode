
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AlphabetBook
{
    public class TozzleItemDragHandler : ItemDragHandlerBase
    {
        public Image dropImage;

        [SerializeField]
        private Vector2 beganSize = Vector2.one;

        [SerializeField]
        private Vector2 endSize = Vector2.one;


        protected override void OnBeginDrag()
        {
            base.OnBeginDrag();

           dropImage.raycastTarget = true;
        }

        protected override void OnDrag()
        {
            base.OnDrag();


        }

        protected override void OnEndDrag()
        {
            base.OnEndDrag();

            dropImage.raycastTarget = false;

            Image image = GetComponent<Image>();
            image.raycastTarget = isActive;

            if(isActive)
            {
                image.rectTransform.SetWidth(beganSize.x);
                image.rectTransform.SetHeight(beganSize.y);
            }
            else
            {
                image.rectTransform.SetWidth(endSize.x);
                image.rectTransform.SetHeight(endSize.y);
            }
        }

    }

}
