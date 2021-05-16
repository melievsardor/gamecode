
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace AlphabetBook
{
    public class SortLetters : GameBase
    {

        [SerializeField]
        private List<Text> texts = new List<Text>();

        [SerializeField]
        private List<RectTransform> parents = new List<RectTransform>();

        [SerializeField]
        private RectTransform arrowTransform = null;

        [SerializeField]
        private Transform dropTransform = null;

        [SerializeField]
        private List<Sprite> sprites = new List<Sprite>();

        [SerializeField]
        private RectTransform animalTransform = null;

        [SerializeField]
        private List<float> points = new List<float>();

        private AudioSource audioSource;

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();

            OnReset();
        }


        public void SetLetterText()
        {
            int[] random = Constants.GetRandomIndex(texts.Count);

            for (int i = 0; i < random.Length; i++)
            {
                texts[i].rectTransform.parent = parents[random[i]];
                texts[i].GetComponent<TextDragHandler>().ParentObject = parents[random[i]].gameObject;
                texts[i].rectTransform.anchoredPosition = Vector2.zero;

                texts[i].color = Constants.unselectColor;
            }
        }


        public override void OnCompletedItem()
        {
            base.OnCompletedItem();

            if (index >= dropItems.Count - 1)
            {
                dropItems[0].transform.GetChild(0).DOShakeScale(0.5f, 0.5f, 5, 45f).OnComplete(delegate {

                    dropItems[1].transform.GetChild(0).DOShakeScale(0.5f, 0.5f, 5, 45f).OnComplete(delegate {

                        dropItems[2].transform.GetChild(0).DOShakeScale(0.5f, 0.5f, 5, 45f).OnComplete(delegate {

                            if (dropItems.Count > 3)
                            {
                                dropItems[3].transform.GetChild(0).DOShakeScale(0.5f, 0.5f, 5, 45f).OnComplete(delegate {

                                    if (dropItems.Count > 4)
                                    {
                                        dropItems[4].transform.GetChild(0).DOShakeScale(0.5f, 0.5f, 5, 45f).OnComplete(delegate
                                        {
                                            animalTransform.DOShakeScale(0.5f, 0.3f, 5, 45).OnComplete(delegate
                                            {

                                                gaming.FinishGame();
                                            });
                                        });
                                    }
                                    else
                                    {
                                        animalTransform.DOShakeScale(0.5f, 0.3f, 5, 45).OnComplete(delegate
                                        {

                                            gaming.FinishGame();
                                        });
                                    }

                                });
                            }
                            else
                            {
                                animalTransform.DOShakeScale(0.5f, 0.3f, 5, 45).OnComplete(delegate
                                {
                                    gaming.FinishGame();
                                });
                            }
                        });

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

            dropItems[index].raycastTarget = false;
            dropItems[index].sprite = sprites[index];

            index++;
            if (index >= dropItems.Count - 1)
                index = dropItems.Count - 1;

            dropItems[index].raycastTarget = true;
            dropItems[index].sprite = sprites[index + dropItems.Count];

            arrowTransform.DOAnchorPosX(points[index], 1f);
        }




        public override void OnReset()
        {
            base.OnReset();

            if (Common.GameManager.Instance.setting.IsSound)
                audioSource.Play();

            for (int i = 0; i < dropItems.Count; i++)
            {
                if (i == 0)
                {
                    dropItems[i].raycastTarget = true;
                    dropItems[i].sprite = sprites[dropItems.Count];
                    continue;
                }

                dropItems[i].raycastTarget = false;
                dropItems[i].sprite = sprites[i];
            }

            arrowTransform.DOAnchorPosX(points[0], 1f);

            SetLetterText();


            ShowItems();
        }


        private void ShowItems()
        {
            animalTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(delegate {

                foreach (Transform t in parents)
                {
                    t.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
                }

                foreach (Text t in texts)
                {
                    t.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
                }


                dropTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(delegate {

                    foreach (Image i in dropItems)
                    {
                        i.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
                    }

                    arrowTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
                });



            });
        }


    }

}

