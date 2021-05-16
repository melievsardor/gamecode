using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AlphabetBook
{
    public class ItemButton : MonoBehaviour
    {

        [SerializeField]
        private Image onImage = null;

        [SerializeField]
        private Image offImage = null;

        [SerializeField]
        private Text text = null;

        [SerializeField]
        private ParticleSystem particleSystem = null;

        [SerializeField]
        private Transform cupTransform = null;
        public Transform CupTransform { get { return cupTransform; } }

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }


        public void EnableButton()
        {
            onImage.gameObject.SetActive(true);
            offImage.gameObject.SetActive(false);
         //   text.color = enableColor;
            button.interactable = true;
        }


        public void DisableButton()
        {
            onImage.gameObject.SetActive(false);
            offImage.gameObject.SetActive(true);
         //   text.color = disableColor;
            button.interactable = false;
        }

        public void OpenLock()
        {
            particleSystem.gameObject.SetActive(true);

            Vector2 temp = new Vector2(1.2f, 1.2f);
            onImage.rectTransform.DOScale(temp, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        public void StopAnimButton()
        {
            onImage.DOKill();
            onImage.rectTransform.localScale = Vector3.one;
        }

        public void DisableEffect()
        {
            particleSystem.gameObject.SetActive(false);
            onImage.rectTransform.DOKill();
            onImage.rectTransform.localScale = Vector3.one;
        }



    } // end
}
