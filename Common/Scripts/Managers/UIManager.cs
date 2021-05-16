using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class UIManager : Manager<UIManager>
    {
        [SerializeField] private Loading loading;

        [SerializeField] private GameObject languagePopupPrefab;

        public Events.EventFadeComplete OnFadeComplete;

        private AudioSource audioSource;
        public AudioSource GetAudioSource { get { return audioSource; } }

        public override void Awake()
        {
            base.Awake();

            loading.OnFadeComplete.AddListener(HandleFadeComplete);
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);

            audioSource = GetComponent<AudioSource>();

            if (GameManager.Instance.setting.IsMusic)
                audioSource.Play();

  
          //  Instantiate(languagePopupPrefab);
        }

        private void Start()
        {
            loading.FadeIn();
        }

        void HandleFadeComplete(bool fade)
        {
            OnFadeComplete.Invoke(fade);
        }

        void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
        {
            // pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
        }

        public void HideUI()
        {
            loading.FadeIn();
        }

        public void ShowUI()
        {
            if (GameManager.Instance.setting.IsMusic)
            {
                if(!audioSource.isPlaying)
                    audioSource.Play();
            }
                

            loading.FadeOut();
        }

        public void MainMenuSetProgres(float value)
        {
            loading.SetProgressValue(value);
        }

    }

}

