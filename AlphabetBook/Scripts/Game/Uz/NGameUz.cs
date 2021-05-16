using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace AlphabetBook
{
    public class NGameUz : GameBase
    {

        [SerializeField]
        private RectTransform leftBack = null;
        [SerializeField]
        private RectTransform leftFont = null;
        [SerializeField]
        private RectTransform rightBack = null;
        [SerializeField]
        private RectTransform rightFont = null;

        [SerializeField]
        protected int itemCount = 3;

        [SerializeField]
        private Transform backTransform = null, frontTransform = null;

        public Transform[] itemsTransform;

        [SerializeField]
        private Transform fruitTransform = null;

        [SerializeField]
        private Transform daraxtTransform = null;

        [SerializeField]
        private Transform shadowTransform = null;

        private AudioSource audioSource;

        private int itemsIndex = -1;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        protected override void Start()
        {
            base.Start();

            OnReset();
        }

        protected virtual void SetImages()
        {

            backTransform.localScale = Vector3.zero;
            frontTransform.localScale = Vector3.zero;

            backTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            frontTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

            daraxtTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).OnComplete(delegate
            {
                StartCoroutine(PlayItems());
            });

            shadowTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

        }

        private IEnumerator PlayItems()
        {
            itemsIndex++;

            if (itemsTransform.Length > itemsIndex)
            {
                itemsTransform[itemsIndex].DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

                yield return new WaitForSeconds(0.2f);

                StartCoroutine(PlayItems());
            }
        }

        public override void OnCompletedItem()
        {
            base.OnCompletedItem();

            Vector3 temp = new Vector3(1.1f, 1f, 1f);

            fruitTransform.DOScale(temp, 0.2f);
            backTransform.DOScale(temp, 0.2f);
            frontTransform.DOScale(temp, 0.2f).OnComplete(delegate {

                backTransform.localScale = Vector3.one;
                frontTransform.localScale = Vector3.one;
            });

            fruitTransform.DOMoveY(fruitTransform.position.y - 0.1f, 0.2f);
            backTransform.DOMoveY(backTransform.position.y - 0.1f, 0.2f);
            frontTransform.DOMoveY(frontTransform.position.y - 0.1f, 0.2f).OnComplete(delegate {

                fruitTransform.DOScale(Vector3.one, 0.2f);
                backTransform.DOScale(Vector3.one, 0.2f);
                frontTransform.DOScale(Vector3.one, 0.2f);

                fruitTransform.DOMoveY(fruitTransform.position.y + 0.1f, 0.2f);
                backTransform.DOMoveY(backTransform.position.y + 0.1f, 0.2f);
                frontTransform.DOMoveY(frontTransform.position.y + 0.1f, 0.2f);
            });

            index++;

            if (index == itemCount)
            {
                gaming.FinishGame();
            }
        }

        public override void OnReset()
        {
            base.OnReset();

            if (Common.GameManager.Instance != null && Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();
            }


            SetImages();
        }

        public void OpenBasket()
        {
            leftBack.DOLocalRotate(new Vector3(0f, 0f, 25f), 0.25f);
            leftFont.DOLocalRotate(new Vector3(0f, 0f, 25), 0.25f);

            rightBack.DOLocalRotate(new Vector3(0f, 0f, -25f), 0.25f);
            rightFont.DOLocalRotate(new Vector3(0f, 0f, -25f), 0.25f);
        }

        public void CloseBacket()
        {
            leftBack.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.25f);
            leftFont.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.25f);

            rightBack.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.25f);
            rightFont.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.25f);
        }


    }
}


