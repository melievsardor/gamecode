
using DG.Tweening;
using UnityEngine;

namespace AlphabetBook
{
    public class ImageDropHandler : ItemDropHandlerBase
    {

        protected override void OnDrop()
        {
            base.OnDrop();

            if (ItemDragHandlerBase.itemBeingDrag.tag == this.tag)
            {
                ImageDragHandler imageDrag = ItemDragHandlerBase.itemBeingDrag.GetComponent<ImageDragHandler>();

                imageDrag.transform.SetParent(parentTransform);

                if (GetComponent<UnityEngine.UI.Image>() != null)
                    GetComponent<UnityEngine.UI.Image>().raycastTarget = false;

                if (drop != null)
                    drop.OnCompletedItem();
            }
        }



    }

}

