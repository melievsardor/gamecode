using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace AlphabetBook
{

    public class GameTopBar : MonoBehaviour
    {


        [SerializeField]
        private RectTransform bgTransform = null;


        [SerializeField]
        private RectTransform musicTransform = null;

        [SerializeField]
        private RectTransform soundTransform = null;

        [SerializeField]
        private Image pauseIcon = null;

        [SerializeField]
        private RectTransform pauseIconTransform = null;

        [SerializeField]
        private Image cancelIcon = null;

        [SerializeField]
        private RectTransform cancelIconTransform = null;


        [SerializeField]
        private Image musicOn, musicOff;

        [SerializeField]
        private Image soundOn, soundOff;


        private bool isClick, isStart, isMusic, isSound;

        public void OnClickPause()
        {
            if (isStart)
                return;

            isStart = true;

            isClick = !isClick;

            if (isClick)
            {
                ShowPause();
            }
            else
            {
                HidePause();
            }

        }

        private void ShowPause()
        {
            pauseIconTransform.DORotate(new Vector3(0f, 0f, 30f), 0.1f);
            pauseIcon.DOFade(0f, 0.1f).OnComplete(delegate {

                cancelIconTransform.DORotate(Vector3.zero, 0.1f);
                cancelIcon.DOFade(1f, 0.1f).OnComplete(delegate {

                    bgTransform.DOAnchorPosY(0f, 0.1f).OnComplete(delegate {

                        musicTransform.DOScale(1f, 0.1f).OnComplete(delegate {

                            soundTransform.DOScale(1f, 0.1f).OnComplete(delegate {

                                isStart = false;
                            });

                        });

                    });

                });
            });


        }

        private void HidePause()
        {
            soundTransform.DOScale(0f, 0.1f).OnComplete(delegate
            {

                musicTransform.DOScale(0f, 0.1f).OnComplete(delegate {

                    bgTransform.DOAnchorPosY(236f, 0.1f).OnComplete(delegate {

                        cancelIconTransform.DORotate(new Vector3(0f, 0f, -30f), 0.1f);
                        cancelIcon.DOFade(0f, 0.1f).OnComplete(delegate
                        {

                            pauseIconTransform.DORotate(Vector3.zero, 0.1f);
                            pauseIcon.DOFade(1f, 0.1f).OnComplete(delegate {

                                isStart = false;
                            });
                        });

                    });
                });
            });
        }


        public void ResetPanel()
        {
            DOTween.KillAll();

            soundTransform.localScale = Vector3.zero;
            musicTransform.localScale = Vector3.zero;

            bgTransform.anchoredPosition = new Vector2(bgTransform.anchoredPosition.x, 346f);

            cancelIcon.transform.eulerAngles = new Vector3(0f, 0f, -30f);

            Color cancelColor = cancelIcon.color;
            cancelColor.a = 0f;
            cancelIcon.color = cancelColor;

            pauseIconTransform.transform.eulerAngles = Vector3.zero;

            Color settingColor = pauseIcon.color;
            settingColor.a = 1f;
            pauseIcon.color = settingColor;

            isStart = false;

            isClick = false;
        }


        public void OnClickMusic()
        {
            isMusic = !isMusic;

            if (isMusic)
            {
                musicOn.DOFade(0f, 0.1f).OnComplete(delegate
                {

                    musicOff.DOFade(1f, 0.1f);
                });
            }
            else
            {
                musicOff.DOFade(0f, 0.1f).OnComplete(delegate {

                    musicOn.DOFade(1f, 0.1f);
                });
            }
        }

        public void OnClickSound()
        {
            isSound = !isSound;

            if (isSound)
            {
                soundOn.DOFade(0f, 0.1f).OnComplete(delegate
                {

                    soundOff.DOFade(1f, 0.1f);
                });
            }
            else
            {
                soundOff.DOFade(0f, 0.1f).OnComplete(delegate {

                    soundOn.DOFade(1f, 0.1f);
                });
            }
        }



    }


}

