
using UnityEngine;
using DG.Tweening;
using Lean.Touch;

namespace AlphabetBook
{
    public class Game : MonoBehaviour, IScenes
    {

        [SerializeField]
        private Map map = null;

        [SerializeField]
        private About about = null;

        [SerializeField]
        private Tracing tracing = null;

        [SerializeField]
        private Gaming gaming = null;

        public AudioSource audioSource;

        [SerializeField]
        private AudioClip audioClip = null;

        [SerializeField]
        private Menu menu = null;
        public Menu GetMenu { get { return menu; } }

        [SerializeField]
        private CameraController cameraController = null;

        [SerializeField]
        private LeanTouch leanTouch = null;

        private AudioSource gameAudioSource = null;
        public AudioSource GetGameAudioSource { get { return gameAudioSource; } }

        private void Start()
        {
            gameAudioSource = GetComponent<AudioSource>();
        }

        public void OnClickLetter(int index)
        {
            if (GameManager.Instance.settingPanel.activeSelf)
                return;

            map.GetItemButton(index).DisableEffect();

            if(Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            

            Constants.currentLetter = index;

            GameManager.Instance.alphabetStats.OldButton = index;

            GameManager.Instance.ShowFade(this);

            if (Common.UIManager.Instance != null && Common.UIManager.Instance.GetAudioSource.isPlaying)
            {
                Common.UIManager.Instance.GetAudioSource.Pause();
            }

            cameraController.enabled = false;
            leanTouch.enabled = false;
        }


        public void ShowAbout()
        {
            about.gameObject.SetActive(true);
            tracing.gameObject.SetActive(false);
            gaming.gameObject.SetActive(false);

            about.SetDetail();
        }

        public void ShowTracing()
        {
            about.gameObject.SetActive(false);
            tracing.gameObject.SetActive(true);
            gaming.gameObject.SetActive(false);

            tracing.CreateShape();


            if (Common.GameManager.Instance.setting.IsMusic)
            {
                gameAudioSource.loop = true;
                gameAudioSource.Play();
            }
        }

        public void ShowGaming()
        {
            about.gameObject.SetActive(false);
            tracing.gameObject.SetActive(false);
            gaming.gameObject.SetActive(true);

            gaming.CreateGame();

        }

        public void HideAll()
        {
            about.gameObject.SetActive(false);
            tracing.gameObject.SetActive(false);
            gaming.gameObject.SetActive(false);
        }

        public void ShowMap()
        {
            if (Common.GameManager.Instance.setting.IsMusic)
            {
                Common.UIManager.Instance.GetAudioSource.Play();
            }

            cameraController.enabled = true;
            leanTouch.enabled = true;

            gaming.gameObject.SetActive(false);
            map.gameObject.SetActive(true);
            map.EnableButtons();
            gameObject.SetActive(false);
        }

        public void ShowMenu()
        {
            gaming.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
        }

        public void OnCompleted()
        {
            GameManager.Instance.ResetCamera();

            map.gameObject.SetActive(false);
            gameObject.SetActive(true);

            ShowAbout();
        }
    }
}

