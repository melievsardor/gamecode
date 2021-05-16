using UnityEngine;
using DG.Tweening;

namespace Common
{
    public class Library : MonoBehaviour
    {

        [SerializeField]
        private HorizontalItem itemPrefab;

        [SerializeField]
        private RectTransform contentTransform;

        [SerializeField]
        private Sprite sprite;


        [SerializeField]
        private RectTransform leftTransform = null;

        [SerializeField]
        private RectTransform rightTransform = null;

        private int id;


        private void Awake()
        {
            leftTransform.SetWidth(Screen.width / 2f);
            rightTransform.SetWidth(Screen.width / 2f);
        }

        private void Start()
        {
            // SetItems(2);

            leftTransform.DOAnchorPosX(-leftTransform.GetWidth() - 700f, 2f);
            rightTransform.DOAnchorPosX(leftTransform.GetWidth() + 700f, 2f);
        }

        private void SetItems(int count)
        {
            for (int i = 0; i < count; i++)
            {

                HorizontalItem item = Instantiate(itemPrefab, contentTransform);
                item.SetItems(id++, sprite, 2);
            }
        }

    }
}




