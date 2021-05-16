using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphabetBook
{
    public class BDragHandler : ItemDropHandlerBase
    {

        protected override void OnDrop()
        {
            base.OnDrop();

            if (ItemDragHandlerBase.itemBeingDrag.tag == this.tag)
            {
                ImageDragHandler imageDrag = ItemDragHandlerBase.itemBeingDrag.GetComponent<ImageDragHandler>();

                imageDrag.transform.SetParent(parentTransform);

                if (drop != null)
                    drop.OnCompletedItem();
            }
        }

    }
}


