using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AlphabetBook
{
    public class Gaming : MonoBehaviour, IScenes
    {
        [SerializeField]
        private List<GameObject> gamePrefabs = new List<GameObject>();

        [SerializeField]
        private Transform parentTransform;

        [SerializeField]
        private GameObject winnerPupop = null;

        public GameObject backgroundObject = null;

        [SerializeField]
        private AudioSource audioSource = null;

        [SerializeField]
        private AudioClip clipWinner = null;

        [SerializeField]
        private AudioSource tapAudioSource = null;

        private AudioSource audioSalut;

        private Game game;

        private Camera gameCamera;

        private Color bgColor = new Color(251f / 255f, 250f / 255f, 240f / 255f, 0f);

        private bool isBack;

        private void Start()
        {
            gameCamera = GameObject.FindGameObjectWithTag("GameCamera").GetComponent<Camera>();

            game = GetComponentInParent<Game>();

            audioSalut = GetComponent<AudioSource>();
        }

        public void CreateGame()
        {
            int index = Constants.currentLetter;

            if (parentTransform.childCount > 0)
                Destroy(parentTransform.GetChild(0).gameObject);

            Instantiate(gamePrefabs[index], parentTransform);
        }


        public void FinishGame()
        {
            if(Common.GameManager.Instance.setting.IsMusic)
            {
                audioSource.clip = clipWinner;
                audioSource.Play();

                
            }

            if(game.GetGameAudioSource.isPlaying)
                game.GetGameAudioSource.Pause();


            GameManager.Instance.alphabetStats.NextButton();

            
            StartCoroutine(WaitFinishGame());

            if (Common.GameManager.Instance.setting.IsMusic)
            {
                audioSalut.Play();
            }
            
        }

        private IEnumerator WaitFinishGame()
        {
            yield return new WaitForSeconds(0.5f);

            winnerPupop.SetActive(true);
        }

        public void OnClickNext()
        {
            if(audioSalut.isPlaying)
                audioSalut.Stop();

            if (Common.GameManager.Instance.setting.IsSound)
            {
                tapAudioSource.Play();
            }

            winnerPupop.SetActive(false);
            GameManager.Instance.ShowFade(this);
        }

        public void OnClickRestart()
        {
            if(audioSalut.isPlaying)
                audioSalut.Stop();

            if (Common.GameManager.Instance.setting.IsSound)
            {
                tapAudioSource.Play();
            }

            if (Common.GameManager.Instance.setting.IsMusic)
            {
                game.GetGameAudioSource.Play();
            }

            CreateGame();

            winnerPupop.SetActive(false); 
        }

        public void OnClickBack()
        {
            if (Common.GameManager.Instance.setting.IsSound)
            {
                tapAudioSource.Play();
            }

            DOTween.KillAll();
            isBack = true;
            GameManager.Instance.ShowFade(this);
        }

        public void OnCompleted()
        {

            if (Common.GameManager.Instance.setting.IsMusic)
            {
                if (!Common.UIManager.Instance.GetAudioSource.isPlaying)
                {
                    Common.UIManager.Instance.GetAudioSource.Play();
                }
            }

            gameCamera.backgroundColor = bgColor;

            if (isBack)
            {
                game.ShowTracing();
                isBack = false;
            }
            else
            {
                if (GameManager.Instance.alphabetStats.LevelIndex[0] == GameManager.Instance.alphabetStats.OldButton ||
                    GameManager.Instance.alphabetStats.LevelIndex[1] == GameManager.Instance.alphabetStats.OldButton ||
                    GameManager.Instance.alphabetStats.LevelIndex[2] == GameManager.Instance.alphabetStats.OldButton)
                {
                    game.ShowMenu();
                    GameManager.Instance.ReasetLeanTouch();
                    game.GetMenu.SelectItem();
                }
                else
                {
                    game.ShowMap();
                }
               
            }


        }
    }

}

