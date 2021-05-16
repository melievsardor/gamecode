using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class PGame : GameBase
    {

        [SerializeField]
        private Image[] dragImages = new Image[2];


        [SerializeField]
        private Transform item0Transform = null;

        [SerializeField]
        private Transform item1Transform = null;

        [SerializeField]
        private RectTransform tableTransform = null;

        private AudioSource audioSource;

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();

            OnReset();
        }

        public override void OnCompletedItem()
        {
            base.OnCompletedItem();

            index++;
            index = Mathf.Clamp(index, 0, dropItems.Count);

            if (index >= dropItems.Count)
            {
                item0Transform.DOShakeScale(1f, 0.3f, 5, 15).OnComplete(delegate {

                    item1Transform.DOShakeScale(1f, 0.3f, 3, 15).OnComplete(delegate {

                        gaming.FinishGame();
                    });
	            });
            }
            else
            {
                NextItem();
            }
        }

        protected override void NextItem()
        {
            base.NextItem();

            ShowItem(dragImages[index].rectTransform);
        }

        public override void OnReset()
        {
            base.OnReset();


            if (Common.GameManager.Instance.setting.IsSound)
                audioSource.Play();


            PlayScene();

            NextItem();
        }


        private void PlayScene()
        {
            

            tableTransform.DOAnchorPosX(0f, 0.5f).SetEase(Ease.InBack).OnComplete(delegate {

                NextItem();

                item0Transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                item1Transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

            });
        }


        private void ShowItem(RectTransform rect)
        {
            rect.DOAnchorPosX(0f, 0.3f).SetEase(Ease.InBack);
        }


        private void HideItems()
        {
            foreach (Image image in dragImages)
                image.rectTransform.DOAnchorPosX(300f, 0.2f);
        }


    }

}

