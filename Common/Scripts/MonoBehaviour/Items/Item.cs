using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        [SerializeField] Button button;

        private int id;

        private bool isBlock;

        private void Start()
        {
            button.onClick.AddListener(HandlerButtonClicked);
        }

        private void HandlerButtonClicked()
        {
            if (isBlock)
                return;

            isBlock = true;

            if (id == 0)
            {

                UIManager.Instance.ShowUI();
            }
        }


        public void SetImage(int id, Sprite sprite)
        {
            this.id = id;
            image.sprite = sprite;
        }

        public void OnClick(int id)
        {
            if (isBlock)
                return;

            isBlock = true;
            Constants.bookId = id;

            UIManager.Instance.ShowUI();

        }


    }

}

