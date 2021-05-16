using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class TozzleBase : GameBase
    {
        [SerializeField]
        protected Image[] itemsImage = new Image[4];

        [SerializeField]
        private RectTransform fillTransform = null;

        [SerializeField]
        protected Transform[] itemsParent = new Transform[4];

        private AudioSource audioSource;

        private int count, showIndex = -1;

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();

            SetImages();
        }

        public void OnRestart()
        {
            SetImages();
            count = 0;
        }

        protected virtual void SetImages()
        {

            if (Common.GameManager.Instance.setting.IsMusic)
                audioSource.Play();
        }

        protected void ShowItems()
        {
            fillTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(delegate {

                StartCoroutine(ShowItemRoutine());
    	    });
        }


        private IEnumerator ShowItemRoutine()
        {
            showIndex++;

            if(itemsParent.Length > showIndex)
            {
                itemsParent[showIndex].DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

                yield return new WaitForSeconds(0.2f);

                StartCoroutine(ShowItemRoutine());
            }
            else
            {
                showIndex = -1;
            }
        }

        public override void OnCompletedItem()
        {
            base.OnCompletedItem();

            count += 1;

            if (count == itemsImage.Length)
            {
                fillTransform.DOShakeScale(1f, 0.2f, 3, 10f).OnComplete(delegate
                {
                    gaming.FinishGame();
                });

            }
        }

    }
}


