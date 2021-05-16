using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace AlphabetBook
{
    public class FindBase : GameBase
    {
        [SerializeField]
        private Transform letterTransform;

        [SerializeField]
        protected Button[] buttons = new Button[4];

        [SerializeField]
        private AudioClip questionClip = null;

        [SerializeField]
        private int questionCount = 2;


        [SerializeField]
        protected Sprite[] sprites = new Sprite[8];

        [SerializeField]
        protected AudioClip[] clips = new AudioClip[2];

        [SerializeField]
        private AudioSource actionAudioSource = null;

        [SerializeField]
        private AudioClip answerClip = null, wrongClip = null;

        protected int imageIndex = -4;

        protected RectTransform[] buttonTransform = new RectTransform[4];

        protected Image[] images = new Image[4];

        protected AudioSource audioSource;

        protected int currentQuestion, showItem = -1;


        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();

            for(int i = 0; i < buttons.Length; i++)
            {
                buttonTransform[i] = buttons[i].GetComponent<RectTransform>();

                images[i] = buttons[i].GetComponentInChildren<Image>();
            }

            SetQuestion();
        }

        protected virtual void SetQuestion()
        {
            if (Common.GameManager.Instance.setting.IsSound)
                StartCoroutine(PlaySound());

        }


        protected void ShowItems()
        {
            letterTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(delegate {

                StartCoroutine(ShowItemsRoutine());
            });
        }

        private IEnumerator ShowItemsRoutine()
        {
            showItem++;

            if(showItem < buttonTransform.Length)
            {
                buttonTransform[showItem].DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

                yield return new WaitForSeconds(0.2f);

                StartCoroutine(ShowItemsRoutine());
            }
            else
            {
                showItem = -1;
            }
        }

        public void OnClickItem(bool answer, RectTransform rect, Button button)
        {
            button.interactable = false;

            if(answer)
            {
                rect.DOShakeScale(1f, 0.3f, 3, 15).OnComplete(delegate {

                    rect.localScale = Vector3.one;

                    button.interactable = true;
                });

                if (Common.GameManager.Instance.setting.IsSound)
                {
                    actionAudioSource.clip = answerClip;
                    actionAudioSource.Play();
                }

                currentQuestion++;

                if (currentQuestion < questionCount)
                {
                    foreach (Button b in buttons)
                        b.onClick.RemoveAllListeners();

                    NextQuestion();
                }
                else
                {
                    gaming.FinishGame();
                }
            }
            else
            {
                rect.DOShakeAnchorPos(1f, 25f, 5, 45f).OnComplete(delegate {

                    rect.localScale = Vector3.one;

                    button.interactable = true;
                });

                if (Common.GameManager.Instance.setting.IsSound)
                {
                    actionAudioSource.clip = wrongClip;
                    actionAudioSource.Play();
                }
            }
        }


        private void NextQuestion()
        {
            StartCoroutine(HideItems());
        }

        private IEnumerator HideItems()
        {
            yield return new WaitForSeconds(0.2f);
            showItem++;

            if (showItem < buttonTransform.Length)
            {
                buttonTransform[showItem].DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);

                yield return new WaitForSeconds(0.2f);

                StartCoroutine(HideItems());
            }
            else
            {
                showItem = -1;
                SetQuestion();
            }
        }

        private IEnumerator PlaySound()
        {
            audioSource.clip = questionClip;
            audioSource.Play();

            yield return new WaitForSeconds(questionClip.length);

            QuestionSound();
        }

        private void QuestionSound()
        {
            int clipIndex = Mathf.Clamp(currentQuestion, 0, clips.Length-1);
            audioSource.clip = clips[clipIndex];
            audioSource.Play();

        }


    }
}


