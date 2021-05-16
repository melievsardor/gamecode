using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace AlphabetBook
{
    public class ItemDragHandlerBase : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public static GameObject itemBeingDrag;

        private Vector3 startPosition;
        private Transform startParent;

        private Camera _camera;

        protected bool isActive;

        protected virtual void Start()
        {
            _camera = GameObject.FindGameObjectWithTag("GameCamera").GetComponent<Camera>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            itemBeingDrag = gameObject;
            startPosition = transform.position;
            startParent = transform.parent;

            isActive = false;

            OnBeginDrag();

            transform.DOShakeScale(0.2f, 0.5f, 5, 45f);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 currentPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            currentPosition.z = 0f;

            transform.position = currentPosition;

            OnDrag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemBeingDrag = null;

            if (transform.parent == startParent)
            {
                isActive = true;
                transform.position = startPosition;
            }
            else
            {
                isActive = false;
            }

            OnEndDrag();
        }


        protected virtual void OnBeginDrag()
        {

        }

        protected virtual void OnDrag()
        {

        }

        protected virtual void OnEndDrag()
        {

        }

    }

}


