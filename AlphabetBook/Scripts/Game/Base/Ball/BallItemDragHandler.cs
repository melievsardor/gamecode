using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace AlphabetBook
{
    public class BallItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public static BallItemDragHandler itemBeingDrag;

        [SerializeField]
        private Transform parentTransform = null;

        [SerializeField]
        private Image ballonImage = null;

        [SerializeField]
        private Transform letterTransform = null;
        public Transform LetterTransform { get { return letterTransform; } }

        [SerializeField]
        private string itemTag = "Item0";

        [SerializeField]
        private BallItemDragHandler similarToLetter = null;

        private Transform startParentTranform;

        private Vector3 beganPos, endPos;

        private RectTransform rectTransform;

        private Camera _camera;

        private Image rayImage;

        private BallBase ballBase;

        private bool isDrop;
        public bool IsDrop { get { return isDrop; } set { isDrop = value; } }

        private float xPos;

        private void Start()
        {
            _camera = GameObject.FindGameObjectWithTag("GameCamera").GetComponent<Camera>();

            ballBase = GetComponentInParent<BallBase>();

            rectTransform = parentTransform.GetComponent<RectTransform>();
            rayImage = GetComponent<Image>();
        }


        public void SetValues(float y, float t, Sprite sprite)
        {
            float worldWidth = 10.8f * Screen.width / Screen.height;

            xPos = (worldWidth / 2f) * 100 + 200f;

            beganPos = new Vector3(xPos, y, 0f);
            endPos = new Vector3(-xPos, y, 0f);

            ballonImage.sprite = sprite;

            StartCoroutine(MoveRoutine(t));
        }

        private IEnumerator MoveRoutine(float t)
        {
            yield return new WaitForSeconds(t);

            Move();
        }

        private void Move()
        {
            rectTransform.DOShakePosition(30f, 0.2f, 2, 15f).SetLoops(-1);
            rectTransform.DOShakeRotation(30f, 10f, 2, 15f).SetLoops(-1);
            rectTransform.DOShakeScale(30f, 0.1f, 1, 10f).SetLoops(-1);
            rectTransform.DOAnchorPos(endPos, 30f).From(beganPos).SetLoops(-1);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            rectTransform.DOKill();
            startParentTranform = letterTransform.parent;
            itemBeingDrag = this;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 currentPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            currentPosition.z = 0f;

            rectTransform.position = currentPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (letterTransform.parent == startParentTranform)
            {
                // tushmadi

                BallonAgainMove();
            }
            else
            {
                rayImage.raycastTarget = false;
                ballBase.AddLetterTransform(letterTransform);
                //tushdi
                letterTransform.localPosition = Vector3.zero;
                letterTransform.localEulerAngles = Vector3.zero;
                letterTransform.localScale = Vector3.one;

                if (similarToLetter != null)
                {
                    if (!similarToLetter.IsDrop)
                    {
                        similarToLetter.tag = itemTag;
                        similarToLetter.IsDrop = true;
                    }
                }

                //BallonMove();
                FastMove();
            }
        }// end


        private void BallonAgainMove()
        {
            rectTransform.DOShakePosition(30f, 0.2f, 2, 15f);
            rectTransform.DOShakeRotation(30f, 10f, 2, 15f);
            rectTransform.DOShakeScale(30f, 0.1f, 1, 10f);
            rectTransform.DOAnchorPos(endPos, 30f).OnComplete(delegate {

                Move();
	        });
        }

        private void BallonMove()
        {
            rectTransform.DOShakePosition(30f, 0.2f, 2, 15f);
            rectTransform.DOShakeRotation(30f, 10f, 2, 15f);
            rectTransform.DOShakeScale(30f, 0.1f, 1, 10f);
            rectTransform.DOAnchorPos(endPos, 30f);
        }

        public void FastMove()
        {
            rectTransform.DOScale(Vector3.zero, 1f).OnComplete(delegate {

                gameObject.SetActive(false);
	        });
        }

        public static implicit operator BallItemDragHandler(NewBallItemDragHandler v)
        {
            throw new NotImplementedException();
        }
    }
}


