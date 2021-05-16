using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AlphabetBook
{

    public class OneGame : GameBase
    {
        [SerializeField]
        private List<Text> texts = new List<Text>();

        [SerializeField]
        private List<Transform> textParents = new List<Transform>();

        [SerializeField]
        private RectTransform arrowTransform = null;

        [SerializeField]
        private List<Sprite> sprites = new List<Sprite>();

        [SerializeField]
        private RectTransform animalTransform = null;

        [SerializeField]
        private Transform dropTransform = null;

        private AudioSource audioSource;

        [SerializeField]
        private  float[] points = { -535f, -415f, -295f, -175f, -55f, 55f, 175f, 295f, 415f, 535f};

        [SerializeField]
        private AudioClip[] itemAudioClips = new AudioClip[2];


        private int[] random;

        private int dropIndex = -1;

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();

          
            StartCoroutine(PlayAudioRoutane());

            OnReset();
        }


        private IEnumerator PlayAudioRoutane()
        {

            if (Common.GameManager.Instance.setting.IsMusic)
            {
                audioSource.Play();


                yield return new WaitForSeconds(audioSource.clip.length);

                if (itemAudioClips.Length > 0)
                {
                    audioSource.clip = itemAudioClips[0];
                    audioSource.Play();

                    yield return new WaitForSeconds(audioSource.clip.length);

                    audioSource.clip = itemAudioClips[1];
                    audioSource.Play();
                }
               
            }
               
        }

        public void SetLetterText()
        {
            random = Constants.GetRandomIndex(8);

            for (int i = 0; i < random.Length; i++)
            {
                texts[i].rectTransform.parent = textParents[random[i]];

                texts[i].rectTransform.anchoredPosition = Vector2.zero;

                texts[i].GetComponent<TextDragHandler>().ParentObject = textParents[random[i]].gameObject;

                texts[i].color = Constants.unselectColor;
            }
        }


        public override void OnCompletedItem()
        {
            base.OnCompletedItem();

            if (index >= dropItems.Count - 1)
            {
                Constants.GetItemIndex = 0;
                Constants.GetNewItemIndex = 0;

                //StartCoroutine(ItemsShake());
                animalTransform.DOShakeScale(0.2f, 0.3f, 5, 45).OnComplete(delegate
                {
                    gaming.FinishGame();
                });
            }
            else
            {
                NextItem();
            }
        }

        private IEnumerator ItemsShake()
        {
            dropIndex++;

            if(texts.Count > dropIndex)
            {
                texts[dropIndex].transform.DOShakeScale(0.2f, 0.5f, 5, 15);

                yield return new WaitForSeconds(0.2f);

                StartCoroutine(ItemsShake());
            }
            else
            {
                animalTransform.DOShakeScale(0.2f, 0.3f, 5, 45).OnComplete(delegate
                {
                    gaming.FinishGame();
                });
            }

        }

        public void SetText(Transform t)
        {
            int temp = dropItems.Count - 8;

            if (index <= temp)
            {
                textParents[8 + index - 1].GetComponentInChildren<TextDragHandler>().GetPosition().position = t.position;
                textParents[8 + index - 1].GetComponentInChildren<TextDragHandler>().GetPosition().gameObject.SetActive(true);
            }
        }

        protected override void NextItem()
        {
            base.NextItem();

            dropItems[index].raycastTarget = false;
            dropItems[index].sprite = sprites[index];

            index++;
            if (index >= dropItems.Count)
                index = dropItems.Count - 1;

            dropItems[index].raycastTarget = true;
            dropItems[index].sprite = sprites[index + texts.Count];

            arrowTransform.DOAnchorPosX(points[index], 1f);
        }

        public override void OnReset()
        {
            base.OnReset();

            for (int i = 0; i < dropItems.Count; i++)
            {
                if (i == 0)
                {
                    dropItems[i].raycastTarget = true;
                    dropItems[i].sprite = sprites[texts.Count];
                    continue;
                }

                dropItems[i].raycastTarget = false;
                dropItems[i].sprite = sprites[i];
            }

            arrowTransform.DOAnchorPosX(points[0], 1f);

            SetLetterText();

            animalTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(delegate {
	
                foreach(Transform t in textParents)
                {
                    t.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
                }

                foreach(Text t in texts)
                {
                    t.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
                }

              

	        });

            dropTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

            foreach (Image i in dropItems)
            {
                i.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            }

            arrowTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

        }
    }//end class

}
