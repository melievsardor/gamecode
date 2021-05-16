using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class DrawBase : GameBase
    {
        [SerializeField]
        private Image[] lines = new Image[3];


        [SerializeField]
        private Button[] buttons = new Button[3];

        [SerializeField]
        private Image[] buttonImages = new Image[4];

        [SerializeField]
        private GameObject finishObject = null;

        [SerializeField]
        private Transform shakeTransform = null;

        [SerializeField]
        private GameObject hideObject = null;

        [SerializeField]
        private GameObject hideObject1 = null;

        [SerializeField]
        private GameObject dotPrefab = null;

        [SerializeField]
        private int dotCount = 41;

        [SerializeField]
        private Sprite[] buttonSprite = new Sprite[2]; 

        private AudioSource audioSource = null;

        private List<Vector2Int> dotPoints = new List<Vector2Int>();

        private List<Vector2> brushPoints = new List<Vector2>();

        private bool isFinish, isBegan, isCheck;

        protected bool isDraw;

        protected override void Start()
        {
            base.Start();

            if(buttons.Length > 0)
            {
                buttons[0].transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).From(Vector3.one).SetLoops(-1, LoopType.Yoyo);
            }

            audioSource = GetComponent<AudioSource>();

            OnReset();
        }

        private void Update()
        {
            if (isFinish)
                return;

            if (!isDraw)
                return;

            if (Input.GetMouseButton(0))
            {
                Vector2 mouesePosition = GetCurrentPlatformClickPosition();

                RaycastHit2D hit = Physics2D.Raycast(mouesePosition, Vector3.up);

                if (hit.collider != null && hit.collider.CompareTag("Collider"))
                {
                    isBegan = true;

                    Vector2Int temp = new Vector2Int((int)mouesePosition.x, (int)mouesePosition.y);

                    if (dotPoints.Contains(temp) == false)
                    {
                        dotPoints.Add(temp);
                    }

                    if(brushPoints.Contains(mouesePosition) == false)
                    {
                        brushPoints.Add(mouesePosition);

                        Instantiate(dotPrefab, mouesePosition, Quaternion.identity, transform);
                    }

                    // hit.transform.GetComponent<SpriteMask>().enabled = true;
                }

            }

            if (Input.GetMouseButtonUp(0) && isBegan)
            {
                CheckFinish();
            }

        }

        private void CheckFinish()
        {
            
            if (dotPoints.Count >= dotCount)
            {
                isFinish = true;

                shakeTransform.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;

                hideObject.SetActive(false);

                shakeTransform.DOShakeScale(1f, 0.2f, 5, 10f).OnComplete(delegate {

                    gaming.FinishGame();

                    StartCoroutine(WaitFinishGame());
                });
            }

            isBegan = false;
        }

        private IEnumerator WaitFinishGame()
        {
            yield return new WaitForSeconds(0.5f);

            gameObject.SetActive(false);
        }


        public override void OnReset()
        {
            base.OnReset();

            dotPoints.Clear();

            brushPoints.Clear();

            if(Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == 0)
                {
                    buttons[i].transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).From(Vector3.one).SetLoops(-1, LoopType.Yoyo);
                    buttons[i].interactable = true;
                    buttonImages[i].sprite = buttonSprite[0];
                }
                else
                {
                    buttons[i].transform.DOKill();
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].interactable = false;
                    buttonImages[i].sprite = buttonSprite[1];
                }

                lines[i].gameObject.SetActive(false);
            }

            isFinish = false;

            if (buttons.Length > 0)
            {
                canvas.gameObject.SetActive(true);
                finishObject.SetActive(false);
            }
            else
            {
                canvas.gameObject.SetActive(false);
                finishObject.SetActive(true);

                isDraw = true;
            }



        }

        public void OnClickedHandler(int index)
        {
            lines[index].gameObject.SetActive(true);

            buttons[index].gameObject.SetActive(false);

            FillCoroutine(lines[index], index);

        }

        private void FillCoroutine(Image image, int index)
        {
            StartCoroutine(AutoFillRoutine(image, index));
        }

        private IEnumerator AutoFillRoutine(Image image, int index)
        {
            while (image.fillAmount < 1)
            {
                image.fillAmount += 0.02f;
                yield return new WaitForSeconds(0.001f);
            }

            index += 1;

            if (index < buttons.Length)
            {
                buttons[index].interactable = true;
                buttonImages[index].sprite = buttonSprite[0];
                buttons[index].transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).From(Vector3.one).SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                DrawFinish();
            }
        }

        private void DrawFinish()
        {
            buttons[buttons.Length - 1].gameObject.SetActive(false);

            for (int i = 0; i < buttons.Length; i++)
            {
                if (i != buttons.Length - 1)
                    lines[i].gameObject.SetActive(false);
            }

            canvas.gameObject.SetActive(false);
            finishObject.SetActive(true);

            hideObject.GetComponent<SpriteRenderer>().DOFade(1f, 1f);

            isDraw = true;
        }


        private Vector3 GetCurrentPlatformClickPosition()
        {
            Vector3 cPosition = Vector3.zero;

            if (Application.isMobilePlatform)
            {
                //current platform is mobile
                if (Input.touchCount != 0)
                {
                    Touch touch = Input.GetTouch(0);
                    cPosition = touch.position;
                }
            }
            else
            {
                //others
                cPosition = Input.mousePosition;
            }

            cPosition = gameCamera.ScreenToWorldPoint(cPosition);//get click position in the world space
            cPosition.z = 0;



            return cPosition;
        }

    }
}


