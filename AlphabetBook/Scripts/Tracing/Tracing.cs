using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace AlphabetBook
{
    public class Tracing : MonoBehaviour, IScenes, IActionTracingAudio
    {
        [SerializeField]
        private List<PlayerTracing> shapes = new List<PlayerTracing>();

        [SerializeField]
        private Transform parentTransform = null;

        [SerializeField]
        private NextPupop nextPupop = null;


        [SerializeField]
        private AudioSource audioSource = null;

        [SerializeField]
        private AudioClip clipNext = null;

        [SerializeField]
        private AudioSource actionAudioSource = null;

        [SerializeField]
        private AudioClip[] actionClips = new AudioClip[2];

        private PlayerTracing letterObject;

        private Game game;

        private bool isBack;

        int index;

        private void Start()
        {
            game = GetComponentInParent<Game>();
        }

        public void CreateShape()
        {
            nextPupop.Hide();

            index = Constants.currentLetter;

            if (parentTransform.childCount > 0)
            {
                Destroy(parentTransform.GetChild(0).gameObject);
            }

            letterObject = Instantiate(shapes[index], parentTransform);


            letterObject.SetActionAudio = this;

            letterObject.transform.localScale = Vector3.zero;

            letterObject.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack);

        }

        public void ShapeCompleted()
        {
            nextPupop.Show(1);


            if(Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.clip = clipNext;
                audioSource.Play();
            }
            
        }

        public void OnClickNext()
        {
            nextPupop.Hide();
            GameManager.Instance.ShowFade(this);
        }

        public void OnClickRestart()
        {
            nextPupop.Hide();

            if (Common.GameManager.Instance.setting.IsMusic)
            {
                game.GetGameAudioSource.Play();
            }

            letterObject.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(delegate {

                CreateShape();
            });

        }


        public void OnClickBack()
        {
            isBack = true;
            GameManager.Instance.ShowFade(this);
        }

        public void OnCompleted()
        {
            if (isBack)
            {
                game.ShowAbout();
                isBack = false;
            }
            else
            {
                game.ShowGaming();
            }
            
        }

        public void PlayAudio()
        {
            if (Common.GameManager.Instance.setting.IsSound)
            {
                actionAudioSource.clip = actionClips[2];
                actionAudioSource.loop = true;
                actionAudioSource.Play();
            }
        }

        public void StopAudio()
        {
            if (actionAudioSource.isPlaying)
            {
                actionAudioSource.loop = false;

                actionAudioSource.Stop();
            }
        }

        public void PlayMalades()
        {
            if (Common.GameManager.Instance.setting.IsSound)
            {
                actionAudioSource.clip = actionClips[3];
                actionAudioSource.Play();
            }
        }
    }

}

