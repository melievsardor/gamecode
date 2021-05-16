using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphabetBook
{
    public class TDropHandler : ItemDropHandlerBase
    {
        private IItemDropHandler itemDrop;

        private void Start()
        {
            itemDrop = GetComponentInParent<IItemDropHandler>();
        }

        protected override void OnDrop()
        {
            base.OnDrop();

            if (ItemDragHandlerBase.itemBeingDrag.tag == this.tag)
            {
                ItemDragHandlerBase.itemBeingDrag.transform.SetParent(parentTransform);

                itemDrop.OnCompletedItem();
            }


        }

    }
}


