
using UnityEngine;

namespace AlphabetBook
{
    public class IGame : TozzleBase
    {

        protected override void SetImages()
        {
            base.SetImages();

            int[] random = Constants.GetRandomIndex(itemsImage.Length);

            for (int i = 0; i < random.Length; i++)
            {
                itemsImage[random[i]].rectTransform.SetParent(itemsParent[i]);
                itemsImage[random[i]].rectTransform.anchoredPosition = Vector2.zero;
                itemsImage[random[i]].rectTransform.localScale = Vector3.one;

                itemsImage[random[i]].GetComponent<TozzleItemDragHandler>().dropImage = dropItems[random[i]];
            }

            ShowItems();
        }


    }

}

