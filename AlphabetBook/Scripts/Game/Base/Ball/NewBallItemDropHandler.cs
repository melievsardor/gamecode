using System.Collections;
using System.Collections.Generic;
using AlphabetBook;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBallItemDropHandler : MonoBehaviour, IDropHandler
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
        if (NewBallItemDragHandler.itemBeingDrag.tag == this.tag)
        {
            NewBallItemDragHandler.itemBeingDrag.LetterTransform.SetParent(parent);

            ballBase.OnCompletedItem();
        }
    }// end

}
