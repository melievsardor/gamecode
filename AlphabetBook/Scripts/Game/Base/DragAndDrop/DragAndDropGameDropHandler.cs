
using UnityEngine;


namespace AlphabetBook
{
    public class DragAndDropGameDropHandler : ItemDropHandlerBase
    {
        protected override void OnDrop()
        {
            base.OnDrop();

            if (ItemDragHandlerBase.itemBeingDrag != null && ItemDragHandlerBase.itemBeingDrag.tag == "Yes")
            {
                ItemDragHandlerBase.itemBeingDrag.transform.SetParent(parentTransform);

                float itemX = ItemDragHandlerBase.itemBeingDrag.GetComponent<RectTransform>().anchoredPosition.x;

                float x = Mathf.Clamp(itemX, -100f, 100f);

                ItemDragHandlerBase.itemBeingDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 25f);

                drop.OnCompletedItem();
            }
        }

    }
}


