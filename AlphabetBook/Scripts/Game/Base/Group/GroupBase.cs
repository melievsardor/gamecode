using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class GroupBase : GameBase
    {

        [SerializeField]
        private Image[] dragImages = new Image[4];

        [SerializeField]
        private RectTransform[] dragParent = new RectTransform[4];

        [SerializeField]
        private RectTransform[] dropParent = new RectTransform[4];

        [SerializeField]
        private Image[] helperDropImages = new Image[4];

        [SerializeField]
        private RectTransform[] helperDropParent = new RectTransform[4];

        [SerializeField]
        private RectTransform[] dropSortFill = new RectTransform[4];

        private AudioSource audioSource;

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();

            SetItems();
        }

        private void SetItems()
        {
            if(Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();
            }

            int[] random = Constants.GetRandomIndex(dragImages.Length);

            for (int i = 0; i < random.Length; i++)
            {
                dragImages[random[i]].rectTransform.SetParent(dragParent[i]);
                dragImages[random[i]].rectTransform.anchoredPosition = Vector2.zero;

                dragImages[random[i]].GetComponent<GroupDragHandler>().parentObject = dragParent[i].gameObject;
            }

            int[] dropRandom = Constants.GetRandomIndex(dropItems.Count);

            for (int i = 0; i < dropRandom.Length; i++)
            {
                dropItems[dropRandom[i]].rectTransform.SetParent(dropParent[i]);
                dropItems[dropRandom[i]].rectTransform.anchoredPosition = Vector2.zero;
            }

            for (int i = 0; i < dropRandom.Length; i++)
            {
                helperDropImages[dropRandom[i]].rectTransform.SetParent(helperDropParent[i]);
                helperDropImages[dropRandom[i]].rectTransform.anchoredPosition = Vector2.zero;
            }


            foreach(Image i in dragImages)
            {
                i.transform.DOScale(Vector3.one, 0.5f).SetDelay(0.1f).SetEase(Ease.OutBack);
            }

            foreach (RectTransform i in dropParent)
            {
                i.parent.DOScale(Vector3.one, 0.5f).SetDelay(0.1f).SetEase(Ease.OutBack);
            }


            foreach (RectTransform i in dropParent)
            {
                i.DOScale(Vector3.one, 0.5f).SetDelay(0.1f).SetEase(Ease.OutBack);
            }

            foreach (Image i in dropItems)
            {
                i.rectTransform.DOScale(Vector3.one, 0.5f).SetDelay(0.1f).SetEase(Ease.OutBack);
            }
        }


        public override void OnCompletedItem()
        {
            base.OnCompletedItem();

            index++;


            if (index == 3)
            {
                EndGame();
            }
        }

        public void ResetImages()
        {
            for (int i = 0; i < dragImages.Length; i++)
            {
                dragImages[i].rectTransform.anchoredPosition = Vector2.zero;
            }
        }

        private void EndGame()
        {
            foreach (Image i in dropItems)
                i.enabled = false;

            dropSortFill[0].transform.DOShakeScale(1f, 0.5f, 5, 45).OnComplete(delegate {


                dropSortFill[1].transform.DOShakeScale(1f, 0.5f, 5, 45).OnComplete(delegate {


                    dropSortFill[2].transform.DOShakeScale(1f, 0.5f, 5, 45).OnComplete(delegate {


                        gaming.FinishGame();

                    });

                });

            });
        }


    }

}

