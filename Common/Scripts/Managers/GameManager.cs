using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


namespace Common
{
    public class GameManager : Manager<GameManager>
    {
        public enum GameState
        {
            PREGAME,
            RUNNING,
            Gaming,
            PAUSE,
            POSTGAME
        }

        public GameObject[] systemPrefab;

        public Setting setting;

        [SerializeField] private AudioSource audioSource = null;
        public AudioSource GetAudioSource { get { return audioSource; } }

        [SerializeField]
        private AudioClip bgClip;

        public Events.EventGameState OnGameStateChanged;

        private List<GameObject> instancedSystemPrefab;

        private GameState currentGameState = GameState.PREGAME;

        private ButtonsController buttonsController;
        public ButtonsController GetButtonsController { get { return buttonsController; } }

        private string currentSceneName = "Boot";

        public GameState CurrentGameState
        {
            get { return currentGameState; }
            private set { currentGameState = value; }
        }


        private void Start()
        {
           // instancedSystemPrefab = new List<GameObject>();

          //  InstantiateSystemPrefab();

            UIManager.Instance.OnFadeComplete.AddListener(HandleFadeComplete);

            buttonsController = GetComponent<ButtonsController>();
        }


        private void OnLoadOperationComplete(AsyncOperation ao)
        {
            switch (currentSceneName)
            {
                case "Boot":
                    UpdateState(GameState.PREGAME);
                    break;
                case "Main":
                    UpdateState(GameState.RUNNING);
                    break;
                default:
                    UpdateState(GameState.Gaming);
                    break;
            }
        }

        private void HandleFadeComplete(bool fade)
        {
            if (!fade)
                return;

            if (currentSceneName == "Boot")
            {
                LoadScene("Main");
            }
            else if (currentSceneName == "Main")
            {
                switch(Constants.bookId)
                {
                    case 0:
                       
                        LoadScene("AlphabetRu");
                        //LoadScene("AlphabetRuPro");
                        break;
                    case 1:
                        
                        LoadScene("AlphabetUz");
                        //LoadScene("AlphabetUzPro");
                        break;
                    case 2:

                        LoadScene("MathRu");
                        //LoadScene("MathRuPro");
                        break;
                    case 3:

                        LoadScene("MathUz");
                        //LoadScene("MathUzPro");
                        break;
                }

            }
            else
            {
                LoadScene("Main");
            }
        }

        private void OnUnloadOperationComplete(AsyncOperation ao)
        {

        }

        private void UpdateState(GameState gameState)
        {
            GameState previousGameState = currentGameState;
            currentGameState = gameState;

            switch (currentGameState)
            {
                case GameState.PREGAME:
                    Time.timeScale = 1.0f;
                    break;

                case GameState.RUNNING:
                    Time.timeScale = 1.0f;
                    break;

                case GameState.PAUSE:
                    Time.timeScale = 0.0f;
                    break;

                default:
                    break;
            }

            OnGameStateChanged.Invoke(currentGameState, previousGameState);
        }

        private void InstantiateSystemPrefab()
        {
            GameObject prefabInstance;
            for (int i = 0; i < systemPrefab.Length; ++i)
            {
                prefabInstance = Instantiate(systemPrefab[i]);
                instancedSystemPrefab.Add(prefabInstance);
            }
        }

        public void LoadScene(string sceneName)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            if (ao == null)
            {
                Debug.LogError("[GameManager] Unable to load Scene " + sceneName);
                return;
            }

            StartCoroutine(LoadSceneRoutine(ao));

            ao.completed += OnLoadOperationComplete;

            currentSceneName = sceneName;
        }


        private IEnumerator LoadSceneRoutine(AsyncOperation async)
        {
            yield return null;

            while (!async.isDone)
            {

                UIManager.Instance.MainMenuSetProgres(Mathf.Clamp01(async.progress / 0.9f));

                yield return null;
            }

        }



        public void UnloadScene(string sceneName)
        {
            AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneName);

            if (ao == null)
            {
                Debug.LogError("[GameManager] Unable to load Scene " + sceneName);
                return;
            }

            ao.completed += OnUnloadOperationComplete;
        }

        protected void OnDestroy()
        {
            if (instancedSystemPrefab == null)
                return;

            for (int i = 0; i < instancedSystemPrefab.Count; ++i)
            {
                Destroy(instancedSystemPrefab[i]);
            }

            instancedSystemPrefab.Clear();
        }

        public void TogglePause()
        {
            UpdateState(currentGameState == GameState.RUNNING ? GameState.PAUSE : GameState.RUNNING);
        }


        public void RestartGame()
        {
            UpdateState(GameState.PREGAME);
        }

        public void QuitGame()
        {
            //implement features for quitting

            Application.Quit();
        }



        public IEnumerator EndGame()
        {
            UpdateState(GameState.POSTGAME);
            yield return new WaitForSeconds(1.5f);
            UIManager.Instance.HideUI();
            // SceneManager.LoadScene("GameOver");
        }

        public void RestartFromEndGame()
        {
            SceneManager.LoadScene("Main");
            // Instance.InitSessions();
            UIManager.Instance.ShowUI();
            RestartGame();
        }

        #region Stats

        private void InitSessions()
        {
            //StatsManager.SaveFilePath = Path.Combine(Application.persistentDataPath, "saveGame.json");
            //StatsManager.LoadSessions();
            //CurrentSession = new SessionStats();
        }

        //public void SaveSession(EndGameState endGameState)
        //{
        //    CurrentSession.SessionDate = DateTime.Now.ToLongDateString();
        //    CurrentSession.HighestLevel = hero.GetCurrentLevel();
        //    CurrentSession.WinOrLoss = endGameState;
        //    CurrentSession.ExperienceGained = hero.GetCurrentXP();

        //    StatsManager.sessionKeeper.Sessions.Add(CurrentSession);
        //    StatsManager.SaveSessions();
        //}

        #endregion
    }

}
