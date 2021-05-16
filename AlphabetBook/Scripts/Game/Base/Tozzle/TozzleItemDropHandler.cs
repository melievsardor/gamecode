
using UnityEngine;

namespace AlphabetBook
{
    public class TozzleItemDropHandler : ItemDropHandlerBase
    {
        protected override void OnDrop()
        {
            base.OnDrop();

            if (ItemDragHandlerBase.itemBeingDrag.tag == this.tag)
            {
                ItemDragHandlerBase.itemBeingDrag.transform.SetParent(parentTransform);
                ItemDragHandlerBase.itemBeingDrag.transform.localScale = Vector3.one;

                ItemDragHandlerBase.itemBeingDrag.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

                if (drop != null)
                    drop.OnCompletedItem();
            }
        }///
    }

}

