using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class ZeroGame : GameBase
    {

        [SerializeField]
        private Image[] dragImages = new Image[2];


        [SerializeField]
        private RectTransform mouseTransform = null;

        [SerializeField]
        private GameObject finishObject = null;

        [SerializeField]
        private Transform mouseTailTransform = null;

        [SerializeField]
        private Transform mouseShapeObject = null;

        [SerializeField]
        private Image shapeImage = null;

        [SerializeField]
        private GameObject tailObject = null;

        [SerializeField]
        private Image cheeseShapeImage = null;

        [SerializeField]
        private Image cheeseMaskImage = null;


        [SerializeField]
        private RectTransform plateTransform = null;

        [SerializeField]
        private RectTransform cheeseTransform = null;

        [SerializeField]
        private RectTransform mouseShapeTransform = null;

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
                tableTransform.DOAnchorPosX(500f, 0.5f);

                cheeseShapeImage.enabled = false;

                mouseTransform.DOAnchorPosX(0f, 1f).OnComplete(delegate {

                    StartCoroutine(WiatFinish());
                });
            }
            else
            {
                mouseTailTransform.gameObject.SetActive(false);
                shapeImage.enabled = false;
                tailObject.SetActive(true);
                NextItem();
            }
        }

        private IEnumerator WiatFinish()
        {

            yield return new WaitForSeconds(1f);

            cheeseMaskImage.enabled = true;
            dragImages[1].enabled = false;
            cheeseMaskImage.transform.DOShakeScale(0.2f, 0.2f, 2, 25f);
            mouseTransform.gameObject.SetActive(false);
            finishObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            finishObject.transform.DOShakeScale(1f, 0.3f, 3, 15f).OnComplete(delegate
            {

                gaming.FinishGame();
            });
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


            HideItems();

            PlayScene();

        }


        private void PlayScene()
        {
            mouseShapeTransform.DOAnchorPosY(0f, 0.3f).SetEase(Ease.InBack).OnComplete(delegate {

                plateTransform.DOAnchorPosX(0f, 0.3f).SetEase(Ease.InBack).OnComplete(delegate {

                    cheeseTransform.DOAnchorPosY(0f, 0.3f).SetEase(Ease.InBack).OnComplete(delegate
                    {

                        tableTransform.DOAnchorPosX(0f, 0.3f).SetEase(Ease.InBack).OnComplete(delegate {

                            ShowItem(dragImages[0].rectTransform);

                        });

                    });

	            });

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

