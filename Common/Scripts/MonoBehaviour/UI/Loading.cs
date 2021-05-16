using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class Loading : MonoBehaviour
    {
        public Events.EventFadeComplete OnFadeComplete;


        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private Slider slider = null;

        [SerializeField] private GameObject imageRU = null;

        [SerializeField] private GameObject imageUz = null;

        [SerializeField] private Image bgImage = null;

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        }

        public void OnFadeOutComplete()
        {
            OnFadeComplete.Invoke(true);
        }

        public void OnFadeInComplete()
        {
            if (GameManager.Instance.CurrentGameState == GameManager.GameState.PREGAME)
            {
                UIManager.Instance.ShowUI();
            }

            bgImage.raycastTarget = false;

        }


        void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
        {
            FadeIn();
        }

        public void FadeIn()
        {
            canvasGroup.DOFade(0, 0.2f).From(1).onComplete += OnFadeInComplete;
            
        }

        public void FadeOut()
        {
            bgImage.raycastTarget = true;

            imageRU.SetActive(Constants.bookId % 2 == 0);
            imageUz.SetActive(Constants.bookId % 2 != 0);

            canvasGroup.DOFade(1, 0.2f).From(0).onComplete += OnFadeOutComplete;
        }

        public void SetProgressValue(float value)
        {
            slider.value = value;
        }



    }

}

