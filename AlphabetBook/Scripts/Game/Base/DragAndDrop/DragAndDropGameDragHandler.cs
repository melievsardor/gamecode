

using UnityEngine.UI;

namespace AlphabetBook
{
    public class DragAndDropGameDragHandler : ItemDragHandlerBase
    {
        private DragAndDropBase dragGame;

        private NGameUz nGameUz;

        protected override void Start()
        {
            base.Start();

            dragGame = FindObjectOfType<DragAndDropBase>();

            if(dragGame == null)
            {
                nGameUz = FindObjectOfType<NGameUz>();
            }
        }



        protected override void OnBeginDrag()
        {
            base.OnBeginDrag();

            if (dragGame != null)
                dragGame.OpenBasket();
            else if (nGameUz != null)
                nGameUz.OpenBasket();

        }




        protected override void OnEndDrag()
        {
            base.OnEndDrag();


            GetComponent<Image>().raycastTarget = isActive;

            if (dragGame != null)
                dragGame.CloseBacket();
            else if (nGameUz != null)
                nGameUz.CloseBacket();
        }


    }

}

