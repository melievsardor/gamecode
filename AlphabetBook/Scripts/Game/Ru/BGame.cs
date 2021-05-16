using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AlphabetBook
{
    public class BGame : GameBase
    {

        [SerializeField]
        private GameObject object0;

        [SerializeField]
        private GameObject object1;

        [SerializeField]
        private Transform obj0Transform = null;

        [SerializeField]
        private Transform[] text0Transform = null;

        [SerializeField]
        private Transform[] drop0Transform = null;

        [SerializeField]
        private Transform obj1Transform = null;

        [SerializeField]
        private Transform[] text1Transform = null;

        [SerializeField]
        private Transform[] drop1Transform = null;

        [SerializeField]
        private int[] countIndex = { 4, 8 };

        [SerializeField]
        private AudioClip[] audioClips = new AudioClip[2];

        private AudioSource audioSource;

        private int count;

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();

            if(Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }

            obj0Transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(delegate {

                foreach (Transform t in text0Transform)
                    t.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

                foreach (Transform t in drop0Transform)
                    t.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
	        });
        }

        public override void OnCompletedItem()
        {
            base.OnCompletedItem();

            count++;

            if (count == countIndex[0])
            {

                obj0Transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(delegate {

                    foreach (Transform t in text0Transform)
                        t.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);

                    foreach (Transform t in drop0Transform)
                        t.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);

                });

                StartCoroutine(WaitShow());
            }

            if (count == countIndex[1])
            {
                gaming.FinishGame();
            }

        }

        private IEnumerator WaitShow()
        {
            yield return new WaitForSeconds(0.8f);

            object0.SetActive(false);
            object1.SetActive(true);

            if (Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.clip = audioClips[1];
                audioSource.Play();
            }

            obj1Transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(delegate {

                foreach (Transform t in text1Transform)
                    t.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

                foreach (Transform t in drop1Transform)
                    t.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            });
        }


    }

}

