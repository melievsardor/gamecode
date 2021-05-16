
using System.Collections.Generic;
using UnityEngine;

namespace AlphabetBook
{
    public class Map : MonoBehaviour, IScenes
    {
        public CameraController camController;


        [SerializeField]
        private List<ItemButton> buttons = new List<ItemButton>();
        

        [SerializeField]
        private Menu menu = null;

        [SerializeField]
        private float[] points = new float[4];

        [SerializeField]
        private Vector2Int[] pointIndex = new Vector2Int[4];

        public List<GameObject> mapBgs = new List<GameObject>();


        private CupProgress cupProgress;

        private void Awake()
        {
            cupProgress = GetComponentInChildren<CupProgress>();

            cupProgress.Load();
            EnableButtons();
        }

        public void EnableButtons()
        {
            camController.enabled = true;

            int level = Constants.tempLevelIndex;

            int startIndex = pointIndex[level].x;
            int endIndex = pointIndex[level].y;

            camController.widthLenght = points[level];
            

            ShowMapBg();

            if (GameManager.Instance.alphabetStats.CurrentButton < endIndex)
            {
                endIndex = GameManager.Instance.alphabetStats.CurrentButton;
            }

            GameManager.Instance.alphabetStats.OldButton = endIndex - 1;

            for (int i = startIndex; i < endIndex; i++)
            {
                buttons[i].EnableButton();
            }

            CameraCenterActiveButton();

            buttons[endIndex - 1].OpenLock();

            if (GameManager.Instance.alphabetStats.IsOpenNewLetter &&
                !GameManager.Instance.alphabetStats.IsFinish)
            {
                cupProgress.SetScore(buttons[GameManager.Instance.alphabetStats.CurrentButton - 2].CupTransform);
            }

            //if(GameManager.Instance.alphabetStats.IsFinish)
            //{
            //    GameManager.Instance.FinishPupopShow();
            //}
        }

        private void ShowMapBg()
        {
            for(int i = 0; i < mapBgs.Count; i++)
            {
                if(i == Constants.tempLevelIndex)
                {
                    mapBgs[i].SetActive(true);
                }
                else
                {
                    mapBgs[i].SetActive(false);
                }
            }
        }

        public ItemButton GetItemButton(int index)
        {
            return buttons[index];
        }

        public void OnClickBack()
        {
            GameManager.Instance.ShowFade(this);
        }

        public void OnCompleted()
        {
            GameManager.Instance.ShowMap(false);
            
        }


        private void CameraCenterActiveButton()
        {
            if(GameManager.Instance.alphabetStats.IsRU)
            {
                switch(Constants.tempLevelIndex)
                {
                    case 0:
                        switch (GameManager.Instance.alphabetStats.OldButton)
                        {
                            case 2:
                                camController.SetCameraPos(1);
                                break;
                            case 3:
                                camController.SetCameraPos(2);
                                break;
                            case 4:
                                camController.SetCameraPos(6);
                                break;
                            case 5:
                                camController.SetCameraPos(7);
                                break;
                            case 6:
                                camController.SetCameraPos(12);
                                break;

                            case 7:
                                camController.SetCameraPos(13);
                                break;
                            default:
                                camController.SetCameraDefault();
                                break;

                        }
                        break;
                    case 1:
                        switch (GameManager.Instance.alphabetStats.OldButton)
                        {
                            case 10:
                                camController.SetCameraPos(1);
                                break;
                            case 11:
                                camController.SetCameraPos(2);
                                break;
                            case 12:
                                camController.SetCameraPos(6);
                                break;
                            case 13:
                                camController.SetCameraPos(7);
                                break;
                            case 14:
                                camController.SetCameraPos(12);
                                break;

                            case 15:
                                camController.SetCameraPos(13);
                                break;
                            case 16:
                                camController.SetCameraPos(16);
                                break;
                            case 17:
                                camController.SetCameraPos(17);
                                break;
                            case 18:
                                camController.SetCameraPos(19);
                                break;
                            case 19:
                                camController.SetCameraPos(25);
                                break;
                            default:
                                camController.SetCameraDefault();
                                break;

                        }
                        break;
                    case 2:
                        switch (GameManager.Instance.alphabetStats.OldButton)
                        {
                            case 22:
                                camController.SetCameraPos(1);
                                break;
                            case 23:
                                camController.SetCameraPos(2);
                                break;
                            case 24:
                                camController.SetCameraPos(6);
                                break;
                            case 25:
                                camController.SetCameraPos(7);
                                break;
                            case 26:
                                camController.SetCameraPos(8);
                                break;
                            default:
                                camController.SetCameraDefault();
                                break;
                        }
                        break;
                    case 3:
                        switch (GameManager.Instance.alphabetStats.OldButton)
                        {
                            case 29:
                                camController.SetCameraPos(1);
                                break;
                            case 30:
                                camController.SetCameraPos(2);
                                break;
                            case 31:
                                camController.SetCameraPos(6);
                                break;
                            case 32:
                                camController.SetCameraPos(7);
                                break;
                            default:
                                camController.SetCameraDefault();
                                break;

                        }
                        break;
                }
            }
            else
            {
                switch (Constants.tempLevelIndex)
                {
                    case 0:
                        switch (GameManager.Instance.alphabetStats.OldButton)
                        {
                            case 2:
                                camController.SetCameraPos(1);
                                break;
                            case 3:
                                camController.SetCameraPos(2);
                                break;
                            case 4:
                                camController.SetCameraPos(6);
                                break;
                            case 5:
                                camController.SetCameraPos(7);
                                break;
                            case 6:
                                camController.SetCameraPos(12);
                                break;
                            case 7:
                                camController.SetCameraPos(13);
                                break;
                            case 8:
                                camController.SetCameraPos(13);
                                break;
                            default:
                                camController.SetCameraDefault();
                                break;

                        }
                        break;
                    case 1:
                        switch (GameManager.Instance.alphabetStats.OldButton)
                        {
                            case 11:
                                camController.SetCameraPos(1);
                                break;
                            case 12:
                                camController.SetCameraPos(2);
                                break;
                            case 13:
                                camController.SetCameraPos(6);
                                break;
                            case 14:
                                camController.SetCameraPos(7);
                                break;
                            case 15:
                                camController.SetCameraPos(15);
                                break;
                            default:
                                camController.SetCameraDefault();
                                break;

                        }
                        break;
                    case 2:
                        switch (GameManager.Instance.alphabetStats.OldButton)
                        {
                            case 18:
                                camController.SetCameraPos(1);
                                break;
                            case 19:
                                camController.SetCameraPos(2);
                                break;
                            case 20:
                                camController.SetCameraPos(6);
                                break;
                            case 21:
                                camController.SetCameraPos(7);
                                break;
                            default:
                                camController.SetCameraDefault();
                                break;

                        }
                        break;
                    case 3:
                        switch (GameManager.Instance.alphabetStats.OldButton)
                        {
                            case 24:
                                camController.SetCameraPos(1);
                                break;
                            case 25:
                                camController.SetCameraPos(2);
                                break;
                            case 26:
                                camController.SetCameraPos(6);
                                break;
                            case 27:
                                camController.SetCameraPos(7);
                                break;
                            case 28:
                                camController.SetCameraPos(12);
                                break;
                            case 29:
                                camController.SetCameraPos(13);
                                break;
                            default:
                                camController.SetCameraDefault();
                                break;

                        }
                        break;
                }
            }
           

        }

    }
}


