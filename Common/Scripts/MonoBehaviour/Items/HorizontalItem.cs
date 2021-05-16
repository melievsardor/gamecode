using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class HorizontalItem : MonoBehaviour
    {
        [SerializeField]
        private Item itemPrefab;

        [SerializeField]
        private RectTransform contentTransform;

        public void SetItems(int id, Sprite sprite, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Item item = Instantiate(itemPrefab, contentTransform);
                item.SetImage(id++, sprite);
            }
        }



    }
}


