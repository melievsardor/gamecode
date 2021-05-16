using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AlphabetBook
{
    public class BubbleBase : GameBase
    {
        private readonly float[] yPos = { 250f, 75f, -75f, -250f };

        [SerializeField]
        private List<BubbleItem> items = new List<BubbleItem>();

        [SerializeField]
        private List<Sprite> sprites = new List<Sprite>();

        [SerializeField]
        private List<Image> bgShapeImage = new List<Image>();

        [SerializeField]
        private List<GameObject> shapeObject = new List<GameObject>();

        [SerializeField]
        private List<Sprite> bgDisableSprite = new List<Sprite>();

        [SerializeField]
        private List<Sprite> bgActiveSprite = new List<Sprite>();

        [SerializeField]
        private List<Transform> letterTransform = new List<Transform>();

        [SerializeField]
        private Image objectFill = null;

        [SerializeField]
        private ParticleSystem particle = null;
        public ParticleSystem GetParticleSystem { get { return particle; } }

        private AudioSource audioSource;

        private int currentIndex = 0;
        public int GetCurrentIndex { get { return currentIndex; } }

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();

            SetValues();
        }

        private void SetValues()
        {

            if (Common.GameManager.Instance.setting.IsSound)
                audioSource.Play();

            float t = 0;

            int[] random = Constants.GetRandomIndex(items.Count);
            int[] subRandom = Constants.GetRandomIndex(yPos.Length);
            int[] spriteRandom = Constants.GetRandomIndex(sprites.Count);

            int subIndex = 0, spriteIndex = 0;

            for(int i = 0; i < random.Length; i++)
            {

                items[random[i]].SetValues(yPos[subRandom[subIndex]], t, sprites[spriteRandom[spriteIndex++]]);
                t += 2.5f;

                subIndex++;

                if (i % 4 == 0)
                {
                    subRandom = Constants.GetRandomIndex(yPos.Length);
                  
                    subIndex = 0;
                }

                if (i % sprites.Count == 0)
                    spriteIndex = 0;
            }

            bgShapeImage[currentIndex].sprite = bgActiveSprite[currentIndex];
            shapeObject[currentIndex].SetActive(true);

            objectFill.DOFade(0.5f, 0.1f);
        }

        public override void OnCompletedItem()
        {
            base.OnCompletedItem();

            //bgShapeImage[currentIndex].sprite = bgDisableSprite[currentIndex];
           // shapeObject[currentIndex].SetActive(false);

            if (currentIndex >= letterTransform.Count - 1)
            {
                DOTween.KillAll();

                foreach (BubbleItem item in items)
                    item.gameObject.SetActive(false);

                objectFill.DOFade(1f, 1f).SetEase(Ease.InOutBounce).OnComplete(delegate {

                    objectFill.transform.DOShakeScale(0.5f, 0.5f, 5, 45);

                    letterTransform[0].DOShakeScale(0.3f, 0.5f, 5, 45f).OnComplete(delegate
                    {

                        letterTransform[1].DOShakeScale(0.3f, 0.5f, 5, 45f).OnComplete(delegate
                        {

                            letterTransform[2].DOShakeScale(0.3f, 0.5f, 5, 45f).OnComplete(delegate
                            {
                                if(letterTransform.Count == 3)
                                {
                                    gaming.FinishGame();
                                  
                                }
                                else
                                {
                                    letterTransform[3].DOShakeScale(0.3f, 0.5f, 5, 45f).OnComplete(delegate
                                    {

                                        gaming.FinishGame();
                                    });
                                }
                               
                            });
                        });
                    });
                });// object
            }
            else
            {
               // shapeObject[currentIndex].SetActive(false);
                currentIndex++;
             //   bgShapeImage[currentIndex].sprite = bgActiveSprite[currentIndex];
             //   shapeObject[currentIndex].SetActive(true);

            }



        }



    }
}


