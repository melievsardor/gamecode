
using UnityEngine;

namespace AlphabetBook
{
    public class TextDropHandler : ItemDropHandlerBase
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

                //ItemDragHandlerBase.itemBeingDrag.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

                itemDrop.OnCompletedItem();
            }
        }


    }
}


