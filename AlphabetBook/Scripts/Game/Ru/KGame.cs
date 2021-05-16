using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class KGame : GameBase, IItemClick
    {

        [SerializeField]
        private Transform cloud0 = null;

        [SerializeField]
        private Transform cloud1 = null;

        [SerializeField]
        private Transform cloud2 = null;


        [SerializeField]
        private Transform homeTransform = null;


        [SerializeField]
        private Transform water0 = null;

        [SerializeField]
        private Transform water1 = null;

        [SerializeField]
        private float speed = 5f;

        [SerializeField]
        private Transform item0 = null;

        [SerializeField]
        private Transform item1 = null;

        [SerializeField]
        private Transform groundTransform = null;

        [SerializeField]
        private Transform homeShowTransform = null;

        [SerializeField]
        private Transform item0ShowTransform = null;

        [SerializeField]
        private Transform item1ShowTransform = null;

        private AudioSource audioSource;

        private float xValue;

        private int count;

        public void ItemShow(int index)
        {
            if(index == 0)
            {
                count++;
                item0.DOMoveX(6f, 2f);
            }
            else if(index == 1)
            {
                count++;
                item1.DOLocalMoveX(5.5f, 1f);
            }

            if (count == 2)
                StartCoroutine(WaitFinish());

        }


        private IEnumerator WaitFinish()
        {
            yield return new WaitForSeconds(2f);

            gaming.FinishGame();
        }

        protected override void Start()
        {
            base.Start();


            audioSource = GetComponent<AudioSource>();

            if (Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();
            }


            cloud0.DOMoveX(-18f, Random.Range(45, 55)).SetLoops(-1, LoopType.Restart).SetDelay(Random.Range(0, 3)).SetEase(Ease.Linear);
           // cloud1.DOMoveX(-18f, Random.Range(65, 75)).SetLoops(-1, LoopType.Restart).SetDelay(Random.Range(3, 6)).SetEase(Ease.Linear);
            cloud2.DOMoveX(18f, Random.Range(35, 45)).SetLoops(-1, LoopType.Restart).SetDelay(Random.Range(6, 12)).SetEase(Ease.Linear);

            homeTransform.DORotate(new Vector3(0f, 0f, 1f), 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutFlash);


            groundTransform.DOMoveY(0f, 0.5f).SetEase(Ease.InBounce);

            homeShowTransform.DOMoveY(0f, 0.5f).SetEase(Ease.InBounce).SetDelay(0.2f).OnComplete(delegate {

                item1ShowTransform.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutBounce);
                item0ShowTransform.DOMoveX(0f, 0.5f).SetEase(Ease.OutBounce);
	        });



        }


        private void Update()
        {
            xValue =  Time.deltaTime / speed;

            Vector3 w0 = water0.position;

            w0.x -= xValue;

            //  
            if (w0.x < -57.62f)
            {
                w0.x = 57.62f;
            }

            water0.position = w0;

            

            Vector3 w1 = water1.position;

            w1.x -= xValue;

            if (w1.x < -57.62f)
            {
                w1.x = 57.62f;
            }

            water1.position = w1;


            
        }


    }

}

