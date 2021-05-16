using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AlphabetBook
{
    public class TouchBase : GameBase, IItemClick
    {

        [SerializeField]
        protected Transform[] items = new Transform[2];


        [SerializeField]
        private AudioClip[] audioClips = new AudioClip[2];

        protected AudioSource audioSource;

        protected int count, playIndex = -1;

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();

            StartCoroutine(PlayAudioRoutine());

            StartCoroutine(PlayAnimation());

        }

        private IEnumerator PlayAudioRoutine()
        {
            if (Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();

                yield return new WaitForSeconds(audioSource.clip.length);

                if(audioClips.Length > 0)
                {
                    audioSource.clip = audioClips[0];
                    audioSource.Play();

                    yield return new WaitForSeconds(audioSource.clip.length);

                    audioSource.clip = audioClips[1];
                    audioSource.Play();
                }
                
            }
        }

        private IEnumerator PlayAnimation()
        {
            playIndex++;

            if(playIndex < items.Length)
            {
                items[playIndex].DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

                yield return new WaitForSeconds(0.5f);

                StartCoroutine(PlayAnimation());
            }
        }

        public void ItemShow(int index)
        {
            count = Mathf.Clamp(count + 1, 0, items.Length);

            if (items.Length == count)
                StartCoroutine(WaitForFinish());

        }

        protected IEnumerator WaitForFinish()
        {
            yield return new WaitForSeconds(1f);

            gaming.FinishGame();
        }



    }

}

