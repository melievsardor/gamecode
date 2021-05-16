using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class BubbleItem : MonoBehaviour
    {
        [SerializeField]
        private Image bubbleImage = null;

        [SerializeField]
        private Button button = null;

        [SerializeField]
        private RectTransform letterTransform = null;

        private RectTransform rectTransform;

        private Vector3 beganPos, endPos;

        [SerializeField]
        private Transform parentTransform = null;

        [SerializeField]
        private ParticleSystem particle = null;

        [SerializeField]
        private string dropTag = "Item0";

        [SerializeField]
        private GameObject shapeObject = null;

        private float waitTime;

        private BubbleBase bubbleBase;


        public void SetValues(float y, float t, Sprite sprite)
        {
            bubbleBase = FindObjectOfType<BubbleBase>();

            float worldWidth = 10.8f * Screen.width / Screen.height;

            float xPos = (worldWidth / 2f) * 100 + 200f;

            beganPos = new Vector3(xPos, y, 0f);
            endPos = new Vector3(-xPos, y, 0f);

            waitTime = t;

            rectTransform = transform.GetComponent<RectTransform>();

            bubbleImage.sprite = sprite;

            StartCoroutine(WaitRoutine());

            button.onClick.AddListener(OnClick);
        }


        private IEnumerator WaitRoutine()
        {
            yield return new WaitForSeconds(waitTime);

            Move();
        }

        private void Move()
        {
            transform.DOShakeRotation(30f, 25f, 2, 25f).SetLoops(-1);
            transform.DOShakeScale(30f, 0.2f, 2, 15f).SetLoops(-1);
            rectTransform.DOAnchorPos(endPos, 30f).From(beganPos).SetLoops(-1);
        }

        public void OnClick()
        {
           // string temp = "Item" + bubbleBase.GetCurrentIndex;
            if (parentTransform == null || this.tag != dropTag)
                return;


            bubbleBase.GetParticleSystem.transform.position = transform.position;
            bubbleBase.GetParticleSystem.Play();

            letterTransform.SetParent(parentTransform);

            rectTransform.DOKill();

            button.interactable = false;

            bubbleImage.enabled = false;

            letterTransform.DOLocalMove(Vector3.zero, 1f).OnComplete(delegate {

                letterTransform.anchoredPosition = Vector2.zero;
                letterTransform.eulerAngles = Vector2.zero;
                letterTransform.localScale = Vector3.one;

                shapeObject.SetActive(false);

                bubbleBase.OnCompletedItem();
            });
        }




    } // end class



}

