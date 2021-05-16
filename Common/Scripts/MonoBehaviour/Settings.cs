
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Common
{
    public class Settings : MonoBehaviour
    {

        [SerializeField]
        private Image musicOn, musicOff;

        [SerializeField]
        private Image soundOn, soundOff;

        [SerializeField]
        private AudioSource aboutAudioSource = null;

        [SerializeField]
        private AudioSource gamingAudioSource = null;

        [SerializeField]
        private AudioSource commonAudioSource = null;

        [SerializeField]
        private GameObject aboutObject = null;

        [SerializeField]
        private GameObject tracingObject = null;

        [SerializeField]
        private GameObject gamingObject = null;

        [SerializeField]
        private AudioSource tracingAudioSource = null;

        [SerializeField]
        private AudioSource tapAudioSource = null;

        [SerializeField]
        private AudioSource mathBgAudioSource = null;

        [SerializeField]
        private bool isMath = false;

        private bool isMusic, isSound;


        public void OnClickOpen()
        {
            gameObject.SetActive(true);

            Load();
        }

        private void Load()
        {

            Color musicOnColor = musicOn.color;
            musicOnColor.a = GameManager.Instance.setting.IsMusic ? 1 : 0;
            musicOn.color = musicOnColor;

            Color musicOffColor = musicOff.color;
            musicOffColor.a = GameManager.Instance.setting.IsMusic ? 0 : 1;
            musicOff.color = musicOffColor;


            Color soundOnColor = soundOn.color;
            soundOnColor.a = GameManager.Instance.setting.IsSound ? 1 : 0;
            soundOn.color = soundOnColor;

            Color soundOffColor = soundOff.color;
            soundOffColor.a = GameManager.Instance.setting.IsSound ? 0 : 1;
            soundOff.color = soundOffColor;

        }


        public void OnClickMusic()
        {
            isMusic = !isMusic;

            GameManager.Instance.setting.IsMusic = !isMusic;

            if (Common.GameManager.Instance.setting.IsSound)
            {
                if(tapAudioSource != null)
                {
                    tapAudioSource.Play();
                }
            }

            if (isMusic)
            {
                if (commonAudioSource != null && commonAudioSource.isPlaying)
                {
                    commonAudioSource.Pause();

                }


                if (aboutAudioSource != null && aboutAudioSource.isPlaying)
                {
                    aboutAudioSource.Stop();
                }

                if (gamingAudioSource != null && gamingAudioSource.isPlaying)
                {
                    gamingAudioSource.Pause();
                }

                if(tracingAudioSource != null && tracingAudioSource.isPlaying)
                {
                    tracingAudioSource.Pause();
                }

                if (isMath)
                {
                    if(mathBgAudioSource.isPlaying)
                        mathBgAudioSource.Pause();
                }
                else
                {
                    if (UIManager.Instance.GetAudioSource.isPlaying)
                    {
                        UIManager.Instance.GetAudioSource.Pause();
                    }
                }
                

                musicOn.DOFade(0f, 0.1f).OnComplete(delegate
                {

                    musicOff.DOFade(1f, 0.1f);
                });
            }
            else
            {
                if(aboutObject != null && aboutObject.activeSelf)
                {
                    if (aboutAudioSource != null && aboutAudioSource.clip != null)
                    {
                        if (UIManager.Instance.GetAudioSource.isPlaying)
                        {
                            UIManager.Instance.GetAudioSource.Pause();
                        }

                        //aboutAudioSource.Play();
                    }
                }
                else if (tracingObject != null && tracingObject.activeSelf)
                {
                    if (tracingAudioSource != null && tracingAudioSource.clip != null)
                    {
                        if (UIManager.Instance.GetAudioSource.isPlaying)
                        {
                            UIManager.Instance.GetAudioSource.Pause();
                        }

                        tracingAudioSource.Play();
                    }
                }
                else if (gamingObject != null && gamingObject.activeSelf)
                {
                    if (gamingAudioSource != null && gamingAudioSource.clip != null)
                    {
                        if(UIManager.Instance.GetAudioSource.isPlaying)
                        {
                            UIManager.Instance.GetAudioSource.Pause();
                        }
                        
                        gamingAudioSource.Play();
                    }
                }
                else
                {
                    if(isMath)
                    {
                        mathBgAudioSource.Play();
                    }
                    else
                    {
                        UIManager.Instance.GetAudioSource.Play();
                    }
                    
                }

                musicOff.DOFade(0f, 0.1f).OnComplete(delegate {

                    musicOn.DOFade(1f, 0.1f);
                });
            }
        }

        public void OnClickSound()
        {
            isSound = !isSound;

            GameManager.Instance.setting.IsSound = !isSound;

            if (Common.GameManager.Instance.setting.IsSound)
            {
                if(tapAudioSource != null)
                {
                    tapAudioSource.Play();
                }
                
            }

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

        public void OnClickCancel()
        {
            gameObject.SetActive(false);
        }



    }
}


