using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AlphabetBook
{
    public class BallBase : GameBase
    {
        private readonly float[] yPos = { 200f, 100f, -100f, -200f };

        [SerializeField]
        private List<NewBallItemDragHandler> items = new List<NewBallItemDragHandler>();

        [SerializeField]
        private List<Sprite> ballonSprites = new List<Sprite>();


        [SerializeField]
        private Transform shapeObject = null;

        [SerializeField]
        private Transform fillObject = null;

        private List<Transform> letterTransform = new List<Transform>();

        [SerializeField]
        private Transform dropBgTransform = null;

        [SerializeField]
        private List<Image> dropHelpers = new List<Image>();

        private AudioSource audioSource;

        private int currentIndex;
        public int CurrentIndex { get { return currentIndex; } }

        private int letterIndex = -1;

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();

            PrepareGame();
        }

        private void PrepareGame()
        {
            if(Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();
            }

            float t = 0f;

            int ballonCount = ballonSprites.Count;
            int[] randomPos = Constants.GetRandomIndex(yPos.Length);
            int[] randomBallon = Constants.GetRandomIndex(ballonCount);
            int[] randomItem = Constants.GetRandomIndex(items.Count);

            int randomIndex = 0, posIndex = 0;

            for (int i = 0; i < items.Count; i++)
            {
                items[randomItem[i]].SetValues(yPos[randomPos[posIndex++]], t, ballonSprites[randomBallon[randomIndex++]]);
                t += 2.5f;

                if (i % yPos.Length == 0)
                    posIndex = 0;

                if (i % randomBallon.Length == 0)
                    randomIndex = 0;
            }


            shapeObject.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(delegate {

                dropBgTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(delegate {

                    foreach (Image p in dropItems)
                        p.transform.parent.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

                    foreach (Image p in dropItems)
                        p.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
                });
	        });


        }



        public override void OnCompletedItem()
        {
            base.OnCompletedItem();

            if (currentIndex >= dropItems.Count - 1)
            {
                foreach (Image i in dropItems)
                    i.enabled = false;

                foreach (NewBallItemDragHandler item in items)
                    item.FastMove();

                shapeObject.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBounce).OnComplete(delegate {


                    fillObject.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce).OnComplete(delegate {

                        fillObject.DOShakeScale(0.5f, 0.3f, 3, 25f).OnComplete(delegate
                        {

                            StartCoroutine(LettersShake());

                        });
                    });


                });
                

            }
            else
            {
                //dropHelpers[currentIndex].raycastTarget = false;
                currentIndex++;
                //dropHelpers[currentIndex].raycastTarget = true;

            }


        }


        private IEnumerator LettersShake()
        {
            yield return new WaitForSeconds(0.3f);

            //letterIndex++;

            //if(letterTransform.Count > letterIndex)
            //{
            //    letterTransform[letterIndex].DOShakeScale(0.3f, 0.5f, 5, 45f);

            //    yield return new WaitForSeconds(0.3f);

            //    StartCoroutine(LettersShake());
            //}
            //else
            //{
                gaming.FinishGame();
          //  }

        }


        public void AddLetterTransform(Transform t)
        {
            letterTransform.Add(t);
        }

    }
}


