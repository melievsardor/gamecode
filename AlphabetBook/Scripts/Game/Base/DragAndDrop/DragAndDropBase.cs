
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace AlphabetBook
{
    public class DragAndDropBase : GameBase
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
        private Transform[] radialTransform = new Transform[10];


        [SerializeField]
        private Animator handAnimator = null;


        private RadialLayout radialLayout;

        private AudioSource audioSource;

        private int itemsIndex = -1;

        private int animaIndex = 0;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            radialLayout = GetComponentInChildren<RadialLayout>();
        }

        protected override void Start()
        {
            base.Start();

            OnReset();

            StartTutorial();
        }

        protected virtual void SetImages()
        {

            backTransform.localScale = Vector3.zero;
            frontTransform.localScale = Vector3.zero;

            backTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
            frontTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(delegate {

                StartCoroutine(PlayItems());
            });

          
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

            for (int i = 0; i < radialTransform.Length - 1; i++)
            {
                if (radialTransform[i].childCount == 0)
                {
                    if(radialTransform[i + 1].childCount == 1)
                    {
                        radialTransform[i + 1].GetChild(0).parent = radialTransform[i];
                        radialTransform[i].GetChild(0).localPosition = Vector3.zero;
                    }
                }
            }

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

            StopTutorial();
        }

        public void CloseBacket()
        {
            leftBack.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.25f);
            leftFont.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.25f);

            rightBack.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.25f);
            rightFont.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.25f);
        }



        private void StartTutorial()
        {
            handAnimator.SetBool("isDrop", true);
        }

        private void StopTutorial()
        {
            handAnimator.SetBool("isDrop", false);

            StartCoroutine(WaitStopTutorial());

        }

        private IEnumerator WaitStopTutorial()
        {
            handAnimator.SetBool("isSwipe", true);

            yield return new WaitForSeconds(2f);

            handAnimator.SetBool("isSwipe", false);

            handAnimator.gameObject.SetActive(false);
        }


    }



}

