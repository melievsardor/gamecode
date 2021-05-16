using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AlphabetBook
{
    public class PlayerTracing : MonoBehaviour
    {
        public string letterName = string.Empty;

        public int pathCount = 3;

        public GameObject fillSprite;

        public new ParticleSystem particleSystem;

        public PolygonCollider2D[] polygonColliders;

        public SpriteRenderer[] pathHelpers;

        public SpriteRenderer[] pathDots;

        public SpriteRenderer[] pathArrows;

        [SerializeField]
        private GameObject handTracing = null;

        public Transform pathTransform;

        protected GameObject lineObject;

        private List<GameObject> brushObjects = new List<GameObject>();

        protected LineRenderer lineRenderer;

        [SerializeField]
        protected GameObject brushPrefabs = null;

        [SerializeField]
        private Transform brushTransform = null;

        [SerializeField]
        private bool isMath;

        private List<Vector2> brushPositions = new List<Vector2>();

        protected Camera _camera;

        protected List<Vector2Int> points = new List<Vector2Int>();

        protected List<Vector2Int> trashPoints = new List<Vector2Int>();

        private Tracing tracing;

        private MathBook.FiveGame fiveGame;

        private Animator animator;

       // private SpriteRenderer shapeRenderer;


        private IActionTracingAudio actionTracingAudio;
        public IActionTracingAudio SetActionAudio { get { return actionTracingAudio; } set { actionTracingAudio = value; } }

        protected int index;

        protected bool isFinish, isStart, isPathCompleted = false;


        protected virtual void Start()
        {
            animator = GetComponent<Animator>();

            //shapeRenderer = GetComponent<SpriteRenderer>();

            if(!isMath)
            {
                brushPrefabs.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

                tracing = GetComponentInParent<Tracing>();

                _camera = GameObject.FindGameObjectWithTag("GameCamera").GetComponent<Camera>();
            }
            else
            {
                brushPrefabs.transform.localScale = new Vector3(0.4f, 0.35f, 0.35f);

                fiveGame = GetComponentInParent<MathBook.FiveGame>();

                _camera = GetComponentInParent<Camera>();
            }
            

            StartTutorial();
        }

        private void StartTutorial()
        {
            index = 0;
            EnableTracingHand();
        }

        private void Update()
        {
            if (isFinish)
                return;


            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }
            else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
            {
                UpdateLine();
            }
            else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
            {
                EndLine();
            }
        }

        protected virtual void CreateLine()
        {
            lineRenderer = null;

            points.Clear();
            trashPoints.Clear();

            if(actionTracingAudio != null)
                actionTracingAudio.PlayAudio();

            ActivePath();

            Vector2 mousePosition = GetCurrentPlatformClickPosition();

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            Vector2Int temp = new Vector2Int((int)mousePosition.x, (int)mousePosition.y);

            if (hit.collider != null && hit.collider.CompareTag("Start"))
            {
                isStart = true;
                if (points.Contains(temp) == false)
                {
                    points.Add(temp);
                }

                if (brushPositions.Contains(mousePosition) == false)
                {
                    brushPositions.Add(mousePosition);

                    lineObject = Instantiate(brushPrefabs, mousePosition, Quaternion.identity, brushTransform);
                    brushObjects.Add(lineObject);
                }
            }
        }

        protected virtual void UpdateLine()
        {
            if (!isStart)
                return;

            Vector2 mousePosition = GetCurrentPlatformClickPosition();

            Vector2Int temp = new Vector2Int((int)mousePosition.x, (int)mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);


            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Collider") && isStart)
                {
                    if (points.Contains(temp) == false)
                    {
                        points.Add(temp);
                    }
                }

                if (brushPositions.Contains(mousePosition) == false)
                {
                    brushPositions.Add(mousePosition);

                    lineObject = Instantiate(brushPrefabs, mousePosition, Quaternion.identity, brushTransform);
                    brushObjects.Add(lineObject);
                }

            }
            else
            {
                if (trashPoints.Contains(temp) == false)
                {
                    trashPoints.Add(temp);
                }
            }

            

        }

        protected virtual void EndLine()
        {
            if(actionTracingAudio != null)
                actionTracingAudio.StopAudio();

            isStart = false;

            isPathCompleted = false;
        }

        protected bool CheckPath(int minCount, int maxCount)
        {
            Debug.Log(points.Count);
            Debug.Log("trash = " + trashPoints.Count);

            if (points.Count >= minCount && points.Count <= maxCount && trashPoints.Count <= 1)
            {
                points.Clear();
                trashPoints.Clear();

                brushObjects.Clear();
                brushPositions.Clear();

                return true;
            }
            else
            {
                WrongTracing();
                return false;
            }
        }

        public void CompletedTracing()
        {
            isFinish = true;

            if (actionTracingAudio != null)
                actionTracingAudio.PlayMalades();

            DisableTracingHand();

            points.Clear();
            trashPoints.Clear();


            Color blackColor = new Color(53f / 255f, 53f / 255f, 53f / 255f, 1f);
            Color blackColorAlpha = new Color(53f / 255f, 53f / 255f, 53f / 255f, 0f);


            brushTransform.gameObject.SetActive(false);
            //if(lineRenderers.Length > 0)
            //{
                //lineRenderers[lineRenderers.Length - 1].DOColor(new Color2(blackColor, blackColor), new Color2(blackColorAlpha, blackColorAlpha), 0.5f).OnComplete(delegate
                //{
                    //for (int i = 0; i < lineRenderers.Length; i++)
                    //{
                    //    Destroy(lineRenderers[i].gameObject);
                    //}

                   // shapeRenderer.enabled = false;

                    fillSprite.SetActive(true);
            fillSprite.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;

                    fillSprite.transform.DOShakePosition(0.5f, 0.2f, 2, 15f).OnComplete(delegate
                    {
                        particleSystem.Play();

                        if(!isMath)
                        {
                            tracing.ShapeCompleted();
                        }
                        else
                        {
                            fiveGame.NumberCompleted();
                        }
                        
                    });


                //});
            //}
            
        }



        public void WrongTracing()
        {
            Color redColor = new Color(255f / 255f, 0f / 255f, 0f / 255f, 1f);
            Color redColorAlpha = new Color(255f / 255f, 0f / 255f, 0f / 255f, 0f);

            points.Clear();
            trashPoints.Clear();

            //lineRenderer.DOColor(new Color2(redColor, redColor), new Color2(redColorAlpha, redColorAlpha), 0.5f).OnComplete(delegate
            //{

                //if (lineObject != null)
                //{
                //    Destroy(lineObject);

                //    //lineRenderer = null;
                //}

            if(brushObjects.Count > 0)
            {
                foreach (GameObject obj in brushObjects)
                    Destroy(obj);

                brushObjects.Clear();
            }

                ActivePath();

                EnableTracingHand();
            //});
        }

        protected virtual void ActivePath()
        {

        }

        protected void NextPath()
        {
            animator.SetBool(GetTitle() + index, false);

            index = Mathf.Clamp(index + 1, 0, pathCount);

            EnableTracingHand();
        }

        public void EnableTracingHand()
        {
            animator.SetTrigger(letterName);

            animator.SetBool(GetTitle() + index, true);

            handTracing.SetActive(true);
        }

        public void DisableTracingHand()
        {
            handTracing.SetActive(false);

            animator.SetBool(GetTitle() + index, false);
        }

        public void AnimFinish()
        {
            DisableTracingHand();
        }

        public string GetTitle()
        {
            return letterName.Split('-')[0];
        }

        protected Vector3 GetCurrentPlatformClickPosition()
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

            cPosition = _camera.ScreenToWorldPoint(cPosition);//get click position in the world space
            cPosition.z = 0;



            return cPosition;
        }

        protected void ShowCollider(int id)
        {
            for (int i = 0; i < polygonColliders.Length; i++)
            {
                if (i == id)
                {
                    polygonColliders[i].gameObject.SetActive(true);
                }
                else
                {
                    polygonColliders[i].gameObject.SetActive(false);
                }
            }

        }

        protected void SpriteFade(SpriteRenderer sprite)
        {
            sprite.DOFade(1f, 0.5f);
        }

        protected void ShowPathsHelper(int id)
        {
            pathHelpers[id].gameObject.SetActive(false);

            SpriteFade(pathHelpers[id + 1]);

            pathDots[id * 2].gameObject.SetActive(true);
            pathDots[id * 2 + 1].gameObject.SetActive(false);
            pathArrows[id].gameObject.SetActive(true);
        }

        protected void PathCompleted(int id)
        {
            if (isPathCompleted)
            {
                NextPath();

                ActivePath();

                ShowPathsHelper(id);
            }
        }
  

    }

}

