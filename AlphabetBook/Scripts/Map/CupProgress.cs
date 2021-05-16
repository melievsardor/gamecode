
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AlphabetBook
{
    public class CupProgress : MonoBehaviour
    {

        [SerializeField]
        private Text scoreText = null;

        [SerializeField]
        private Image[] starImages = new Image[8];

        [SerializeField]
        private RectTransform fillTransform = null;

        [SerializeField]
        private Transform targetTransform = null;


        [SerializeField]
        private float deltaX = 18f;

        private int cupCount;


        public void Load()
        {
            cupCount = GameManager.Instance.alphabetStats.CurrentButton - 1;

            scoreText.text = (cupCount + 1).ToString();

            if (cupCount >= GameManager.Instance.alphabetStats.LevelIndex[0])
            {
                starImages[1].enabled = false;

                starImages[0].rectTransform.DOScale(1f, 1f);
                starImages[0].rectTransform.DORotate(new Vector3(0f, 0f, 360f), 1f);
            }

            if (cupCount >= GameManager.Instance.alphabetStats.LevelIndex[1])
            {
                starImages[3].enabled = false;

                starImages[2].rectTransform.DOScale(1f, 1f);
                starImages[2].rectTransform.DORotate(new Vector3(0f, 0f, 360f), 1f);
            }

            if (cupCount >= GameManager.Instance.alphabetStats.LevelIndex[2])
            {
                starImages[5].enabled = false;

                starImages[4].rectTransform.DOScale(1f, 1f);
                starImages[4].rectTransform.DORotate(new Vector3(0f, 0f, 360f), 1f);
            }

            if (cupCount >= GameManager.Instance.alphabetStats.LevelIndex[3])
            {
                starImages[7].enabled = false;

                starImages[6].rectTransform.DOScale(1f, 1f);
                starImages[6].rectTransform.DORotate(new Vector3(0f, 0f, 360f), 1f);
            }


            Vector2 temp = new Vector2(-260f + cupCount * deltaX, fillTransform.anchoredPosition.y);
            

            if (cupCount + 1 == GameManager.Instance.alphabetStats.ButtonCount)
            {
                scoreText.text = (cupCount + 1).ToString();

                temp = new Vector2(362.5f, fillTransform.anchoredPosition.y);
                fillTransform.DOAnchorPos(temp, 1f);
            }
            else
            {
                fillTransform.DOAnchorPos(temp, 1f);
            }
        }


        public void SetScore(Transform cupTransform)
        {
            cupTransform.gameObject.SetActive(true);

            cupTransform.DOMove(targetTransform.position, 0.5f).OnComplete(delegate {

                targetTransform.DOShakeScale(1f, 0.3f, 3, 45);

               // scoreText.text = (cupCount + 1).ToString();

                cupTransform.gameObject.SetActive(false);
            });

            cupCount = GameManager.Instance.alphabetStats.CurrentButton - 1;

            scoreText.text = (cupCount + 1).ToString();

            if (cupCount >= GameManager.Instance.alphabetStats.LevelIndex[0])
            {
                starImages[1].enabled = false;

                starImages[0].rectTransform.DOScale(1f, 1f);
                starImages[0].rectTransform.DORotate(new Vector3(0f, 0f, 360f), 1f);
            }

            if (cupCount >= GameManager.Instance.alphabetStats.LevelIndex[1])
            {
                starImages[3].enabled = false;

                starImages[2].rectTransform.DOScale(1f, 1f);
                starImages[2].rectTransform.DORotate(new Vector3(0f, 0f, 360f), 1f);
            }

            if (cupCount >= GameManager.Instance.alphabetStats.LevelIndex[2])
            {
                starImages[5].enabled = false;

                starImages[4].rectTransform.DOScale(1f, 1f);
                starImages[4].rectTransform.DORotate(new Vector3(0f, 0f, 360f), 1f);
            }

            if (cupCount >= GameManager.Instance.alphabetStats.LevelIndex[3])
            {
                starImages[7].enabled = false;

                starImages[6].rectTransform.DOScale(1f, 1f);
                starImages[6].rectTransform.DORotate(new Vector3(0f, 0f, 360f), 1f);
            }

            Vector2 temp = new Vector2(-260f + cupCount * deltaX, fillTransform.anchoredPosition.y);
           

            if (cupCount + 1 == GameManager.Instance.alphabetStats.ButtonCount)
            {
                scoreText.text = (cupCount + 1).ToString();

                if(!GameManager.Instance.alphabetStats.IsFinish)
                {
                    GameManager.Instance.FinishPupopShow();
                }

                GameManager.Instance.alphabetStats.IsFinish = true;

                temp = new Vector2(362.5f, fillTransform.anchoredPosition.y);
                fillTransform.DOAnchorPos(temp, 1f);
            }
            else
            {
                fillTransform.DOAnchorPos(temp, 1f);
            }
        }


    }
}


