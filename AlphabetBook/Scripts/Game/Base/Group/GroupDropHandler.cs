using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AlphabetBook
{

    public class GroupDropHandler : ItemDropHandlerBase
    {

        [SerializeField]
        private Image fillImage = null;

        [SerializeField]
        private bool isOne = false;

        private int count;

        protected override void OnDrop()
        {
            base.OnDrop();

            if (ItemDragHandlerBase.itemBeingDrag.tag == this.tag)
            {

                ItemDragHandlerBase.itemBeingDrag.transform.SetParent(parentTransform);

                if (count == 0)
                {
                    if(!isOne)
                    {
                        count++;
                        StartCoroutine(AutoFillRoutine(0.5f));
                    }
                    else
                    {
                        StartCoroutine(AutoFillRoutine(1, true));
                    }
                }
                else
                {
                    StartCoroutine(AutoFillRoutine(1, true));
                }

                
            }

        }


        private IEnumerator AutoFillRoutine(float value, bool end = false)
        {
            while (fillImage.fillAmount < value)
            {
                fillImage.fillAmount += 0.02f;
                yield return new WaitForSeconds(0.001f);
            }

            if(end)
                drop.OnCompletedItem();
        }

    } // end

}

