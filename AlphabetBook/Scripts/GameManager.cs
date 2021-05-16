using System.Collections;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class GameManager : Singleton<GameManager>, IScenes
    {
        [SerializeField]
        private Menu menu = null;

        [SerializeField]
        private Map map = null;

        [SerializeField]
        private Game game = null;

        [SerializeField]
        private CameraController cameraController = null;

        [SerializeField]
        private LeanPitchYaw leanPitchYaw = null;

        [SerializeField]
        private LeanFingerTap leanFingerTap = null;

        [SerializeField]
        private LeanFingerSwipe leanFingerSwipe = null;

        [SerializeField]
        private LeanTouch leanTouch = null;

        public AlphabetStats alphabetStats;

        public Setting setting;

        public GameObject settingPanel;

        [SerializeField]
        private Image fadeImage = null;

        [SerializeField]
        private GameObject finishObject = null;


        protected override void Awake()
        {
            base.Awake();

         //   menu.gameObject.SetActive(true);
        }


        public void ResetCamera()
        {
            cameraController.enabled = false;

            leanPitchYaw.enabled = true;
            leanFingerTap.enabled = true;
            leanFingerSwipe.enabled = true;

            cameraController.transform.position = new Vector3(0f, 0f, -10f);
        }

        public void ReasetLeanTouch()
        {
            leanPitchYaw.enabled = true;
            leanFingerTap.enabled = true;
            leanFingerSwipe.enabled = true;

            leanTouch.enabled = true;
        }

        public void OnClickHome()
        {
            if (game.GetGameAudioSource.isPlaying)
            {
                game.GetGameAudioSource.Stop();
            }

            if (Common.GameManager.Instance.setting.IsMusic)
            {
                if(!Common.UIManager.Instance.GetAudioSource.isPlaying)
                {
                    Common.UIManager.Instance.GetAudioSource.Play();
                }
            }

            leanTouch.enabled = true;

            ShowFade(this);
        }

        public void ShowMap(bool value)
        {
            if (!value)
                ResetCamera();

            menu.gameObject.SetActive(!value);
            map.gameObject.SetActive(value);
        }

        public void ShowFade(IScenes scenes)
        {
            fadeImage.raycastTarget = true;

            StartCoroutine(FadeShowRoutine(scenes));
        }

        public void HideFade(IScenes scenes)
        {
            StartCoroutine(FadeHideRoutine(scenes));
        }

        private IEnumerator FadeShowRoutine(IScenes scenes)
        {
            Color color = fadeImage.color;

            while (fadeImage.color.a < 1)
            {
                color.a += 0.02f;
                fadeImage.color = color;
                yield return new WaitForSeconds(0.0001f);
            }

            HideFade(scenes);
        }

        private IEnumerator FadeHideRoutine(IScenes scenes)
        {
            scenes.OnCompleted();

            Color color = fadeImage.color;

            while (fadeImage.color.a > 0)
            {
                color.a -= 0.02f;
                fadeImage.color = color;
                yield return new WaitForSeconds(0.0001f);
            }

            fadeImage.raycastTarget = false;
        }

        public void OnCompleted()
        {
            ResetCamera();

            menu.gameObject.SetActive(true);
            map.gameObject.SetActive(false);
            game.HideAll();
        }


        public void FinishPupopShow()
        {
            StartCoroutine(FinishRoutine());
        }

        private IEnumerator FinishRoutine()
        {
            finishObject.SetActive(true);

            yield return new WaitForSeconds(2f);

            finishObject.SetActive(false);

        }


    }
}


