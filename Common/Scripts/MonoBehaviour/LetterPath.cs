using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlphabetBook
{
    public class LetterPath : MonoBehaviour
    {
        public GameObject colliderObject;

        public Image fillImage;

        //private SwipeTrail swipeTrail;

        //private void Start()
        //{
        //    swipeTrail = GetComponentInParent<SwipeTrail>();
        //}

        public void CollidersShow(bool value)
        {
            colliderObject.SetActive(value);
        }


        public void Fill()
        {
            StartCoroutine(FillRoutine());
        }


        private IEnumerator FillRoutine()
        {
            while (fillImage.fillAmount < 1)
            {
                fillImage.fillAmount += 0.02f;
                yield return new WaitForSeconds(0.001f);
            }

            // swipeTrail.OnCompleted();
        }

    }

}





