
using UnityEngine;
using DG.Tweening;


namespace AlphabetBook
{
    public class TouchItem : MonoBehaviour
    {

        [SerializeField]
        private int index = 0;

        private AudioSource audioSource = null;

        private IItemClick itemClick;

        private bool isClick;

        private void Start()
        {
            itemClick = GetComponentInParent<IItemClick>();

            audioSource = GetComponent<AudioSource>();
        }

        private void OnMouseDown()
        {
            if (isClick)
                return;

            isClick = true;

            if (audioSource != null)
            {
                if (Common.GameManager.Instance.setting.IsSound)
                {
                    audioSource.Play();
                }

            }

            itemClick.ItemShow(index);

            transform.DOShakeScale(1f, 0.3f, 5, 15f);
        }


    }
}


