using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class SimilarBase : GameBase
    {
        [SerializeField]
        private List<Button> buttons = new List<Button>();

        [SerializeField]
        private List<RectTransform> parentTransforms = new List<RectTransform>();

        [SerializeField]
        private List<Sprite> sprites = new List<Sprite>();

        [SerializeField]
        private List<Sprite> iconSprites = new List<Sprite>();

        private int[] random;

        private RectTransform rect0, rect1;

        [SerializeField]
        private Transform item0Target, item1Target, moveParent;


        [SerializeField]
        private GameObject boyObject = null;
        [SerializeField]
        private GameObject missionObject = null;
        [SerializeField]
        private GameObject gamingObject = null;

        [SerializeField]
        private RectTransform missionItem0RectTransform = null;
        [SerializeField]
        private RectTransform missionItem1RectTransform = null;

        [SerializeField]
        private Text item0Text = null;
        [SerializeField]
        private Text item1Text = null;

        [SerializeField]
        private Transform pivotTransform = null;

        [SerializeField]
        private List<GameObject> effects = new List<GameObject>();

        private AudioSource audioSource;

        private int oldId, item0Count, item1Count;

        private string oldTag, currentTag;

        private bool isLock;

        private readonly Vector3 cardRotate = new Vector3(0f, 180f, 0f);

        private Color bgColor = new Color(92f / 255f, 84f / 255f, 82f / 255f, 0f);

        private Image targetImage0, targetImage1;

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();

            gaming.backgroundObject.SetActive(false);
            gameCamera.backgroundColor = bgColor;

            targetImage0 = item0Target.GetComponent<Image>();
            targetImage1 = item1Target.GetComponent<Image>();

            if (Common.GameManager.Instance.setting.IsMusic)
                audioSource.Play();

            StartCoroutine(MissionRoutine());
        }


        private IEnumerator MissionRoutine()
        {
            boyObject.SetActive(true);
            missionObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            MissionWait();
        }

        private void MissionWait()
        {
            missionItem0RectTransform.DOShakeScale(1f, 0.5f, 5, 45f).OnComplete(delegate {

                missionItem1RectTransform.DOShakeScale(1f, 0.5f, 5, 45f).OnComplete(delegate {

                    boyObject.SetActive(false);
                    missionObject.SetActive(false);

                    gamingObject.SetActive(true);
                    PrepareGame();
                });
            });
        }


        private void PrepareGame()
        {
            random = Constants.GetRandomIndex(buttons.Count);

            for (int i = 0; i < random.Length; i++)
            {
                if (i < 4)
                {
                    buttons[random[i]].tag = "Item0";
                }
                else if (i < 8)
                {
                    buttons[random[i]].tag = "Item1";
                }
            }

            buttons[random[0]].onClick.AddListener(() => OnClickButton(0, parentTransforms[random[0]]));
            buttons[random[1]].onClick.AddListener(() => OnClickButton(1, parentTransforms[random[1]]));
            buttons[random[2]].onClick.AddListener(() => OnClickButton(2, parentTransforms[random[2]]));
            buttons[random[3]].onClick.AddListener(() => OnClickButton(3, parentTransforms[random[3]]));
            buttons[random[4]].onClick.AddListener(() => OnClickButton(4, parentTransforms[random[4]]));
            buttons[random[5]].onClick.AddListener(() => OnClickButton(5, parentTransforms[random[5]]));
            buttons[random[6]].onClick.AddListener(() => OnClickButton(6, parentTransforms[random[6]]));
            buttons[random[7]].onClick.AddListener(() => OnClickButton(7, parentTransforms[random[7]]));
            buttons[random[8]].onClick.AddListener(() => OnClickButton(8, parentTransforms[random[8]]));
            buttons[random[9]].onClick.AddListener(() => OnClickButton(9, parentTransforms[random[9]]));
            buttons[random[10]].onClick.AddListener(() => OnClickButton(10, parentTransforms[random[10]]));
            buttons[random[11]].onClick.AddListener(() => OnClickButton(11, parentTransforms[random[11]]));
            buttons[random[12]].onClick.AddListener(() => OnClickButton(12, parentTransforms[random[12]]));
            buttons[random[13]].onClick.AddListener(() => OnClickButton(13, parentTransforms[random[13]]));
            buttons[random[14]].onClick.AddListener(() => OnClickButton(14, parentTransforms[random[14]]));

            OpenCard();
        }

        private void OpenCard()
        {
            isLock = true;

            parentTransforms[random[0]].DOLocalRotate(cardRotate, 0.3f).OnComplete(delegate {

                SetSprite(0);

                StartCoroutine(OpenCardRoutine(0));

            });

            parentTransforms[random[1]].DOLocalRotate(cardRotate, 0.3f).OnComplete(delegate {

                SetSprite(1);

                StartCoroutine(OpenCardRoutine(1));

            });

            parentTransforms[random[2]].DOLocalRotate(cardRotate, 0.3f).OnComplete(delegate {

                SetSprite(2);

                StartCoroutine(OpenCardRoutine(2));

            });

            parentTransforms[random[3]].DOLocalRotate(cardRotate, 0.3f).OnComplete(delegate {

                SetSprite(3);

                StartCoroutine(OpenCardRoutine(3));

            });

            parentTransforms[random[4]].DOLocalRotate(cardRotate, 0.3f).OnComplete(delegate {

                SetSprite(4);

                StartCoroutine(OpenCardRoutine(4));

            });

            parentTransforms[random[5]].DOLocalRotate(cardRotate, 0.3f).OnComplete(delegate {

                SetSprite(5);

                StartCoroutine(OpenCardRoutine(5));

            });

            parentTransforms[random[6]].DOLocalRotate(cardRotate, 0.3f).OnComplete(delegate {

                SetSprite(6);

                StartCoroutine(OpenCardRoutine(6));

            });

            parentTransforms[random[7]].DOLocalRotate(cardRotate, 0.3f).OnComplete(delegate {

                SetSprite(7);

                StartCoroutine(OpenCardRoutine(7, false));

            });

        }


        private IEnumerator OpenCardRoutine(int id, bool islock = true)
        {
            yield return new WaitForSeconds(8f);

            SetIconLocal(id);

            parentTransforms[random[id]].DOLocalRotate(Vector3.zero, 0.3f).OnComplete(delegate {

                isLock = islock;
            });

        }

        private void OnClickButton(int id, RectTransform rect)
        {
            if (isLock)
                return;

            isLock = true;


            rect.DOLocalRotate(cardRotate, 0.3f).OnComplete(delegate
            {
                SetSprite(id);

                if (id < 8)
                {
                    if (string.IsNullOrEmpty(oldTag))
                    {
                        oldTag = buttons[random[id]].tag;

                        rect0 = rect;
                        oldId = id;
                        isLock = false;
                    }
                    else if (!string.IsNullOrEmpty(oldTag) && string.IsNullOrEmpty(currentTag))
                    {
                        currentTag = buttons[random[id]].tag;

                        rect1 = rect;

                        if (currentTag == oldTag)
                        {
                            buttons[random[oldId]].transform.SetParent(moveParent);
                            buttons[random[id]].transform.SetParent(moveParent);

                            StartCoroutine(MoveWaitRoutine(id));

                            oldTag = string.Empty;
                            currentTag = string.Empty;
                        }
                        else
                        {
                            oldTag = string.Empty;
                            currentTag = string.Empty;

                            StartCoroutine(ResetSpriteRoutine(id));

                        }
                    }
                }
                else
                {
                    StartCoroutine(ResetIconRoutine(id, rect));
                }


            });


        } // end onClickButton

        private IEnumerator MoveWaitRoutine(int id)
        {
            yield return new WaitForSeconds(1f);

            buttons[random[oldId]].transform.DOMove(buttons[random[id]].transform.position, 0.5f).OnComplete(delegate {

                buttons[random[oldId]].gameObject.SetActive(false);

                StartCoroutine(MoveRoutine(id));
            });
            //buttons[random[id]].transform.DOMove(pivotTransform.position, 0.3f).OnComplete(delegate {

            //    StartCoroutine(MoveRoutine(id));
            //});
        }

        private IEnumerator MoveRoutine(int id)
        {
            yield return new WaitForSeconds(1f);

            Vector3 pos = Vector3.zero;

            if (buttons[random[id]].tag == "Item0")
            {
                pos = item0Target.position;
                item0Count++;
            }
            else
            {
                pos = item1Target.position;
                item1Count++;
            }

            effects[random[id]].SetActive(true);

            buttons[random[id]].transform.DORotate(new Vector3(0f, 0f, -15f), 0.3f);

            buttons[random[id]].transform.DOMove(pos, 1f).OnComplete(delegate {

                buttons[random[id]].gameObject.SetActive(false);

                if (buttons[random[id]].tag == "Item0")
                {
                    item0Text.text = item0Count.ToString();
                    Color c0 = targetImage0.color;
                    c0.a = 1f;
                    targetImage0.color = c0;
                }
                else
                {
                    item1Text.text = item1Count.ToString();
                    Color c1 = targetImage1.color;
                    c1.a = 1f;
                    targetImage1.color = c1;
                }

                if (item0Count == 2 && item1Count == 2)
                {
                    item1Target.DOShakeScale(1f, 0.5f, 5, 45f).OnComplete(delegate {

                        item0Target.DOShakeScale(1f, 0.5f, 5, 45f).OnComplete(delegate {

                            gaming.FinishGame();

                        });
                    });


                }
                else
                {
                    isLock = false;
                }
            });

        }

        private IEnumerator ResetSpriteRoutine(int id)
        {
            yield return new WaitForSeconds(1f);

            rect0.DOLocalRotate(Vector3.zero, 0.3f).OnComplete(delegate {

                SetIcon(oldId);
            });

            rect1.DOLocalRotate(Vector3.zero, 0.3f).OnComplete(delegate {

                SetIcon(id);

                isLock = false;
            });
        }

        private IEnumerator ResetIconRoutine(int id, RectTransform rect)
        {
            yield return new WaitForSeconds(1f);

            rect.DOLocalRotate(Vector3.zero, 0.3f).OnComplete(delegate
            {
                SetIcon(id);

                rect1 = null;
                oldTag = string.Empty;
                currentTag = string.Empty;

                if (rect0 != null)
                {
                    rect0.DOLocalRotate(Vector3.zero, 0.3f).OnComplete(delegate
                    {

                        SetIcon(oldId);

                        rect0 = null;
                        isLock = false;
                    });
                }
                else
                {
                    isLock = false;
                }

            });
        }

        private void SetSpriteLocal(int id)
        {
            dropItems[random[id]].sprite = sprites[id];
            dropItems[random[id]].transform.localEulerAngles = Vector3.zero;
        }

        private void SetSprite(int id)
        {
            dropItems[random[id]].sprite = sprites[id];
            dropItems[random[id]].transform.eulerAngles = Vector3.zero;
        }

        private void SetIconLocal(int id)
        {
            dropItems[random[id]].sprite = iconSprites[random[id]];
            dropItems[random[id]].transform.localEulerAngles = Vector3.zero;
        }

        private void SetIcon(int id)
        {
            dropItems[random[id]].sprite = iconSprites[random[id]];
            dropItems[random[id]].transform.eulerAngles = Vector3.zero;
        }
    }

}

