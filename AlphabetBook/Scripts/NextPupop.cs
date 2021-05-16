using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace AlphabetBook
{
    public class NextPupop : MonoBehaviour
    {

        [SerializeField]
        private About about = null;

        [SerializeField]
        private Tracing tracing = null;


        private AudioSource audioSource;

        private RectTransform rectTransform;

        private int index;

        private bool isClick;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            audioSource = GetComponent<AudioSource>();
        }


        public void Show(int index)
        {
            this.index = index;

            rectTransform.DOAnchorPosX(-36f, 0.5f).SetEase(Ease.InOutFlash);
        }

        public void Hide()
        {
            rectTransform.anchoredPosition = new Vector2(600f, 30f);
        }

        public void OnClickNext()
        {
            //if (isClick)
            //    return;

            //isClick = true;

            if (Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();
            }

            Hide();

           switch(index)
            {
                case 0:
                    about.OnClickNext();
                    break;

                case 1:
                    tracing.OnClickNext();
                    break;
            }
        }


        public void OnClickRestart()
        {
            //if (isClick)
            //    return;

            //isClick = true;
            if (Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();
            }

            Hide();

            switch (index)
            {
                case 0:
                    about.OnClickRestart();
                    break;

                case 1:
                    tracing.OnClickRestart();
                    break;
            }
        }

        public void OnClickPrev()
        {
            //if (isClick)
            //    return;

            //isClick = true;

            if (Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();
            }

            Hide();

            switch (index)
            {
                case 0:
                    about.OnClickBack();
                    break;

                case 1:
                    tracing.OnClickBack();
                    break;
            }
        }




    }

}

