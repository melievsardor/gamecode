using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AlphabetBook
{
    public class About : MonoBehaviour, IScenes
    {
        [SerializeField]
        private AboutDetails details = null;

        [SerializeField]
        private Text letterText = null;

        [SerializeField]
        private Image image0 = null;

        [SerializeField]
        private Image image1 = null;

        [SerializeField]
        private Image image2 = null;

        [SerializeField]
        private Image image3 = null;

        [SerializeField]
        private Button button0 = null;

        [SerializeField]
        private Button button1 = null;

        [SerializeField]
        private Button button2 = null;

        [SerializeField]
        private Button button3 = null;

        [SerializeField]
        private NextPupop nextPupop = null;


        [SerializeField]
        private AudioSource audioSource = null;

        [SerializeField]
        private AudioClip clipNext = null;

        [SerializeField]
        private AudioSource aboutAudioSource = null;

        [SerializeField]
        private AudioSource itemAudioSource = null;

        private int item0Count = 0, item1Count = 0, item2Count = 0, item3Count = 0;

        private Game game;

        private RectTransform button0Transform, button1Transform, button2Transform, button3Transform;

        private bool isBack, isCompleted, isLetterPlaying;

        private void Awake()
        {
            game = GetComponentInParent<Game>();

            button0Transform = button0.GetComponent<RectTransform>();
            button1Transform = button1.GetComponent<RectTransform>();
            button2Transform = button2.GetComponent<RectTransform>();
            button3Transform = button3.GetComponent<RectTransform>();

        }

        public void SetDetail()
        {
            nextPupop.Hide();

            int index = Constants.currentLetter;

            if(!GameManager.Instance.alphabetStats.IsRU &&
                GameManager.Instance.alphabetStats.OldButton == GameManager.Instance.alphabetStats.ButtonCount-1)
            {
                letterText.transform.eulerAngles = new Vector3(0f, 0f, 180f);
                letterText.alignment = TextAnchor.LowerCenter;
            }
            else 
            {
                letterText.transform.eulerAngles = Vector3.zero;
                letterText.alignment = TextAnchor.MiddleCenter;
            }

            letterText.transform.localScale = Vector3.zero;

            letterText.text = details.GetDetails[index].letter;

            letterText.rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

            image0.sprite = details.GetDetails[index].sprite0;

            image0.rectTransform.SetWidth(details.GetDetails[index].size0.x);
            image0.rectTransform.SetHeight(details.GetDetails[index].size0.y);

            button0Transform.anchoredPosition = details.GetDetails[index].buttonPos0;
            //button0Transform.SetWidth(details.GetDetails[index].size0.x);

            if (details.GetDetails[index].sprite1 != null)
            {
                image1.sprite = details.GetDetails[index].sprite1;
                image1.rectTransform.SetWidth(details.GetDetails[index].size1.x);
                image1.rectTransform.SetHeight(details.GetDetails[index].size1.y);

              //  button1Transform.SetWidth(details.GetDetails[index].size1.x);
                button1Transform.anchoredPosition = details.GetDetails[index].buttonPos1;

                button1.interactable = true;

                image1.enabled = true;
                item1Count = 0;
            }
            else
            {
                button1Transform.anchoredPosition = new Vector2(1000f, -120f);
                button1.interactable = false;
                image1.enabled = false;
                item1Count = 1;
            }

            if (details.GetDetails[index].sprite2 != null)
            {
                image2.sprite = details.GetDetails[index].sprite2;
                image2.rectTransform.SetWidth(details.GetDetails[index].size2.x);
                image2.rectTransform.SetHeight(details.GetDetails[index].size2.y);

                button2Transform.anchoredPosition = details.GetDetails[index].buttonPos2;
               // button2Transform.SetWidth(details.GetDetails[index].size2.x);

                button2.interactable = true;
                image2.enabled = true;
                item2Count = 0;
            }
            else
            {
                button2Transform.anchoredPosition = new Vector2(1000f, -120f);
                button2.interactable = false;
                image2.enabled = false;
                item2Count = 1;
            }

            if (details.GetDetails[index].sprite3 != null)
            {
                image3.sprite = details.GetDetails[index].sprite3;
                image3.rectTransform.SetWidth(details.GetDetails[index].size3.x);
                image3.rectTransform.SetHeight(details.GetDetails[index].size3.y);

                button3Transform.anchoredPosition = details.GetDetails[index].buttonPos3;
               // button3Transform.SetWidth(details.GetDetails[index].size3.x);

                button3.interactable = true;
                image3.enabled = true;
                item3Count = 0;
            }
            else
            {
                button3Transform.anchoredPosition = new Vector2(1000f, -120f);
                button3.interactable = false;
                image3.enabled = false;
                item3Count = 1;
            }

            aboutAudioSource.clip = details.GetDetails[index].letterClip;

            isLetterPlaying = false;


            if (GameManager.Instance.setting.IsMusic)
            {
                button0Transform.localScale = Vector3.zero;
                button1Transform.localScale = Vector3.zero;
                button2Transform.localScale = Vector3.zero;
                button3Transform.localScale = Vector3.zero;

                aboutAudioSource.Play();
            }

            isCompleted = false;



        }


        private void Update()
        {
            if(!isLetterPlaying)
            {
                if(!aboutAudioSource.isPlaying)
                {
                    isLetterPlaying = true;

                    ShowItems();
                }
            }
        }

        private void ShowItems()
        {
            button0Transform.DOScale(Vector3.one, 1f);
            button1Transform.DOScale(Vector3.one, 1f);
            button2Transform.DOScale(Vector3.one, 1f);
            button3Transform.DOScale(Vector3.one, 1f);
        }


        private void ShowTutorial()
        {

        }

        public void OnClickItemHandler(int index)
        {
            int letterIndex = Constants.currentLetter;

            //if (aboutAudioSource.isPlaying)
            //    return;


            if (index == 0)
            {
                if(GameManager.Instance.setting.IsSound) 
                {
                    aboutAudioSource.clip = details.GetDetails[letterIndex].clip0;
                    aboutAudioSource.Play();
                }

                button0Transform.DOShakeScale(0.2f, 0.5f, 5, 45).OnComplete(delegate
                {
                    button0Transform.localScale = Vector3.one;
                });

                item0Count = Mathf.Clamp(item0Count + 1, 0, 1);
            }

            if (index == 1)
            {
                if (GameManager.Instance.setting.IsSound)
                {
                    aboutAudioSource.clip = details.GetDetails[letterIndex].clip1;
                    aboutAudioSource.Play();
                }

                button1Transform.DOShakeScale(0.2f, 0.5f, 5, 45).OnComplete(delegate
                {

                    button1Transform.localScale = Vector3.one;
                });

                item1Count = Mathf.Clamp(item1Count + 1, 0, 1);
            }

            if (index == 2)
            {
                if (GameManager.Instance.setting.IsSound)
                {
                    aboutAudioSource.clip = details.GetDetails[letterIndex].clip2;
                    aboutAudioSource.Play();
                }

                button2Transform.DOShakeScale(0.2f, 0.5f, 5, 45).OnComplete(delegate
                {

                    button2Transform.localScale = Vector3.one;
                });

                item2Count = Mathf.Clamp(item2Count + 1, 0, 1);
            }

            if (index == 3)
            {
                if (GameManager.Instance.setting.IsSound)
                {
                    aboutAudioSource.clip = details.GetDetails[letterIndex].clip3;
                    aboutAudioSource.Play();
                }

                button3Transform.DOShakeScale(0.2f, 0.5f, 5, 45).OnComplete(delegate
                {

                    button3Transform.localScale = Vector3.one;
                });

                item3Count = Mathf.Clamp(item3Count + 1, 0, 1);
            }

            if (item0Count == 1 && item1Count == 1 && item2Count == 1 && item3Count == 1 && !isCompleted)
            {

                isCompleted = true;

                nextPupop.Show(0);

                if(GameManager.Instance.setting.IsSound)
                {
                    audioSource.clip = clipNext;
                    audioSource.Play();
                }
                
            }
        }


        public void OnClickNext()
        {
            isBack = false;

            GameManager.Instance.ShowFade(this);

            item0Count = 0;

            if (details.GetDetails[Constants.currentLetter].sprite1 != null)
            {
                item1Count = 0;
            }
            else
            {
                item1Count = 1;
            }

            if (details.GetDetails[Constants.currentLetter].sprite2 != null)
            {
                item2Count = 0;
            }
            else
            {
                item2Count = 1;
            }

            if (details.GetDetails[Constants.currentLetter].sprite3 != null)
            {
                item3Count = 0;
            }
            else
            {
                item3Count = 1;
            }

            isCompleted = false;
        }

        public void OnClickRestart()
        {
            nextPupop.Hide();

            aboutAudioSource.clip = details.GetDetails[Constants.currentLetter].letterClip;

            isLetterPlaying = false;

            if (GameManager.Instance.setting.IsMusic)
            {
                aboutAudioSource.Play();
            }

            button0Transform.localScale = Vector3.zero;
            button1Transform.localScale = Vector3.zero;
            button2Transform.localScale = Vector3.zero;
            button3Transform.localScale = Vector3.zero;


            letterText.transform.localScale = Vector3.zero;

            letterText.rectTransform.DOScale(Vector3.one, 1f);

            item0Count = 0;

            if (details.GetDetails[Constants.currentLetter].sprite1 != null)
            {
                item1Count = 0;
            }
            else
            {
                item1Count = 1;
            }

            if (details.GetDetails[Constants.currentLetter].sprite2 != null)
            {
                item2Count = 0;
            }
            else
            {
                item2Count = 1;
            }

            if (details.GetDetails[Constants.currentLetter].sprite3 != null)
            {
                item3Count = 0;
            }
            else
            {
                item3Count = 1;
            }

            isCompleted = false;
        }

        public void OnClickBack()
        {
            isBack = true;
            GameManager.Instance.ShowFade(this);
        }

        public void OnCompleted()
        {
            if(isBack)
            {
                game.ShowMap();
                isBack = false;
            }
            else
            {
                game.ShowTracing();
            }
           
        }
    }
}


