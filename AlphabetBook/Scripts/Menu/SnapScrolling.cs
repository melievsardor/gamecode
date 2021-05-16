using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Lean.Touch;

namespace AlphabetBook
{
    public class SnapScrolling : MonoBehaviour
    {
        [Range(0, 50)]
        public int panCount;

        [Range(0, 500)]
        public int panOffset;


        [Range(0, 20)]
        public float snapSpeed;

        [Range(0, 5f)]
        public float scaleOffset;

        [Range(0f, 20f)]
        public float scaleSpeed;

        public GameObject[] panPrefab;

        public ScrollRect scrollRect;

        public RectTransform cloud0Transform;
        public RectTransform cloud1Transform;

        [SerializeField]
        private AudioSource audioSource = null;

        [SerializeField]
        private AudioClip loopAudio = null;

        [SerializeField]
        private AudioClip endAudio = null;

        [SerializeField]
        private List<CanvasGroup> menuBgs = new List<CanvasGroup>();


        [SerializeField]
        private LeanPitchYaw leanPitchYaw = null;

        private Menu menu;

        private GameObject[] instPan;
        private Vector2[] panPos;
        private Vector2[] panScale;

        private Vector2 contentVector;

        private RectTransform contentRect;
        public int selectPanID, id, tempId;

        public bool isScolling;

        private float alphaValue;

        private void OnEnable()
        {
            leanPitchYaw.swipeDelegate += MoveItem;

            LeanTouch.OnFingerDown += TouchDown;

            LeanTouch.OnFingerUp += TouchUp;
        }

        private void OnDisable()
        {
            leanPitchYaw.swipeDelegate -= MoveItem;

            LeanTouch.OnFingerDown -= TouchDown;

            LeanTouch.OnFingerDown -= TouchUp;
        }

        private void Start()
        {
            contentRect = GetComponent<RectTransform>();

            menu = GetComponentInParent<Menu>();

            instPan = new GameObject[panCount];
            panPos = new Vector2[panCount];
            panScale = new Vector2[panCount];

            //Constants.tempLevelIndex = GameManager.Instance.alphabetStats.Level;

            //leanPitchYaw.itemId = Constants.tempLevelIndex;

            //selectPanID = Constants.tempLevelIndex;

            SelectPadId();

            for (int i = 0; i < panCount; i++)
            {
                // instPan[i] = Instantiate(panPrefab, transform, false);
                if (i == 0) continue;
                float x = panPrefab[i - 1].transform.localPosition.x + panPrefab[i].GetComponent<RectTransform>().GetWidth() + panOffset;
                panPrefab[i].transform.localPosition = new Vector2(x, panPrefab[i].transform.localPosition.y);
                panPos[i] = -panPrefab[i].transform.localPosition;
            }

            StartClouds();
        }



        private void FixedUpdate()
        {
            if (contentRect.anchoredPosition.x >= panPos[0].x && !isScolling || contentRect.anchoredPosition.x <= panPos[panCount - 1].x && !isScolling)
            {
                isScolling = false;
                scrollRect.inertia = false;
            }


            float nearestPos = float.MaxValue;

            for (int i = 0; i < panCount; i++)
            {
                float distance = Mathf.Abs(contentRect.anchoredPosition.x - panPos[i].x);

                if(distance < nearestPos)
                {
                    nearestPos = distance;
                   // selectPanID = i;

                }

                float scale = Mathf.Clamp(1f / (distance / panOffset) * scaleOffset, 0.5f, 1f);

                panScale[i].x = Mathf.SmoothStep(panPrefab[i].transform.localScale.x, scale + 0.2f, scaleSpeed * Time.fixedDeltaTime);
                panScale[i].y = Mathf.SmoothStep(panPrefab[i].transform.localScale.x, scale + 0.2f, scaleSpeed * Time.fixedDeltaTime);

                panPrefab[i].transform.localScale = panScale[i];
            }

            float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);

            if (scrollVelocity < 400 & !isScolling) scrollRect.inertia = false;

            alphaValue = Mathf.Abs(scrollRect.velocity.x);

            AlphaChange();

            if (isScolling || scrollVelocity > 400) return;

            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, panPos[selectPanID].x, snapSpeed * Time.fixedDeltaTime);

            contentRect.anchoredPosition = contentVector;
        }



        public void SelectTap()
        {
            //Debug.Log("pad id = " + selectPanID);
            menu.OnClickedHandler(selectPanID);
        }

        private void TouchDown(LeanFinger leanFinger)
        {
            Scrolling(true);
        }

        private void TouchUp(LeanFinger leanFinger)
        {
            Scrolling(false);
        }

        public void SelectPadId()
        {
            Constants.tempLevelIndex = GameManager.Instance.alphabetStats.Level;

            selectPanID = Constants.tempLevelIndex;

            leanPitchYaw.ItemID = -selectPanID;

            float x = -1100f;
            if((GameManager.Instance.alphabetStats.IsRU && GameManager.Instance.alphabetStats.LevelIndex[0] == GameManager.Instance.alphabetStats.OldButton) ||
                GameManager.Instance.alphabetStats.LevelIndex[0] == GameManager.Instance.alphabetStats.OldButton)
            {
                menuBgs[0].DOFade(0f, 1f);
                menuBgs[1].DOFade(1f, 1f);

                x = -1100f;
            }
            else if ((GameManager.Instance.alphabetStats.IsRU && GameManager.Instance.alphabetStats.LevelIndex[1] == GameManager.Instance.alphabetStats.OldButton) ||
                GameManager.Instance.alphabetStats.LevelIndex[1] == GameManager.Instance.alphabetStats.OldButton)
            {
                menuBgs[1].DOFade(0f, 1f);
                menuBgs[2].DOFade(1f, 1f);

                x = -2200f;
            }
            else if ((GameManager.Instance.alphabetStats.IsRU && GameManager.Instance.alphabetStats.LevelIndex[2] == GameManager.Instance.alphabetStats.OldButton) ||
                GameManager.Instance.alphabetStats.LevelIndex[2] == GameManager.Instance.alphabetStats.OldButton)
            {
                menuBgs[2].DOFade(0f, 1f);
                menuBgs[3].DOFade(1f, 1f);

                x = -3300f;
            }

            contentRect.anchoredPosition = new Vector2(x, contentRect.anchoredPosition.y);
        }


        private void MoveItem(int id)
        {
            selectPanID = id;

            AlphaChange();
        }

        public void Scrolling(bool scroll)
        {
            isScolling = scroll;
            scrollRect.inertia = scroll;

            if (!scroll)
            {
                StartClouds();

                if (tempId != selectPanID)
                {
                    audioSource.loop = false;

                    if (Common.GameManager.Instance.setting.IsSound)
                    {
                        audioSource.clip = endAudio;
                        audioSource.Play();
                    }
                }
            }
            else
            {
                cloud0Transform.DOLocalMoveX(-600f, 0.5f);
                cloud1Transform.DOLocalMoveX(600f, 0.5f);

                tempId = selectPanID;
            }

        }


        private void StartClouds()
        {
            cloud0Transform.DOLocalMoveX(-200f, 0.5f);
            cloud1Transform.DOLocalMoveX(200f, 0.5f);
        }


        private void AlphaChange()
        {
            //if (alphaValue < 1)
            //    return;

            int oldId = id;

            id = selectPanID;

            menuBgs[oldId].DOFade(0f, 1f);
            menuBgs[id].DOFade(1f, 1f);

            
        }


    }//class end

}

