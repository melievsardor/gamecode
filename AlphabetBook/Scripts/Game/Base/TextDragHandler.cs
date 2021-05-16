using UnityEngine;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class TextDragHandler : ItemDragHandlerBase
    {

        private Text text;

        [SerializeField]
        private int minFontSize = 120;
        [SerializeField]
        private int maxFontSize = 120;

        [SerializeField]
        private GameObject parentObject = null;
        public GameObject ParentObject { get { return parentObject; } set { parentObject = value; } }

        [SerializeField]
        private string[] itemTag = { "Item0", "Item1" };

        [SerializeField]
        private TextDragHandler similarToDrag = null;

        [SerializeField]
        private TextDragHandler similarToDrag1 = null;

        [SerializeField]
        private bool isNewItem = false;

        private bool isDrop;
        public bool IsDrop { get { return isDrop; } set { isDrop = value; } }

        public static int itemIndex = -1;

        private OneGame oneGame;

        protected override void Start()
        {
            base.Start();

            text = GetComponent<Text>();

            oneGame = FindObjectOfType<OneGame>();
        }


        protected override void OnBeginDrag()
        {
            base.OnBeginDrag();

            text.color = Constants.selectColor;
            text.fontSize = minFontSize;

            text.raycastTarget = isActive;
        }



        protected override void OnEndDrag()
        {
            base.OnEndDrag();

            text.color = !isActive ? Constants.selectColor : Constants.unselectColor;
            text.raycastTarget = isActive;

            if (!isActive)
            {
                if (parentObject != null)
                {
                    if (oneGame != null)
                        oneGame.SetText(parentObject.transform);

                    if (similarToDrag != null)
                    {
                        int index = Mathf.Clamp(Constants.GetItemIndex, 0, itemTag.Length - 1);

                        if (isNewItem)
                            index = Mathf.Clamp(Constants.GetNewItemIndex, 0, itemTag.Length - 1);

                        similarToDrag.tag = itemTag[index];
                        similarToDrag.IsDrop = true;

                        if(similarToDrag1 != null)
                        {
                            similarToDrag1.tag = itemTag[index];
                            similarToDrag1.IsDrop = true;
                        }
                    }

                    parentObject.SetActive(false);
                }

                this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -10f);

                text.fontSize = minFontSize;

            }
            else
            {
                text.fontSize = maxFontSize;
            }

        }


        public Transform GetPosition()
        {
            return parentObject.transform;
        }


    }
}


