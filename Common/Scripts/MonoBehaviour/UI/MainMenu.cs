using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Common
{
    public class MainMenu : MonoBehaviour
    {
        public Events.EventFadeComplete OnMainMenuFadeComplete;


        [SerializeField] private Image bgImage;

        [SerializeField]
        private GameObject progressBar = null;

        [SerializeField]
        private Slider slider = null;

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        }

        public void OnFadeOutComplete()
        {
            OnMainMenuFadeComplete.Invoke(true);
            progressBar.SetActive(false);
        }

        public void OnFadeInComplete()
        {
            progressBar.SetActive(false);
        }


        void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
        {

            FadeIn();
        }

        public void FadeIn()
        {
            bgImage.DOFade(0, 0.3f).From(1).onComplete += OnFadeInComplete;
        }

        public void FadeOut()
        {
            bgImage.DOFade(1, 0.3f).From(0).onComplete += OnFadeOutComplete;
        }

        public void SetProgressValue(float value)
        {
            slider.value = value;
        }


    }

}

