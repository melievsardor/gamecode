using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AlphabetBook
{
    public class NGame : GameBase
    {

        [SerializeField]
        private RectTransform objectTransform = null;

        [SerializeField]
        private Transform item0Transform = null;

        [SerializeField]
        private Transform item1Transform = null;

        [SerializeField]
        private Transform text0Transform = null;

        [SerializeField]
        private Transform text1Transform = null;

        [SerializeField]
        private Transform box0Transform = null;

        [SerializeField]
        private Transform box1Transform = null;

        private AudioSource audioSource;

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();
                
            OnReset();
        }


        public override void OnCompletedItem()
        {
            base.OnCompletedItem();

            index++;
            index = Mathf.Clamp(index, 0, dropItems.Count);

            if (index >= dropItems.Count)
            {

                item0Transform.DOShakeScale(0.5f, 0.3f, 3, 15).OnComplete(delegate {

                    item1Transform.DOShakeScale(0.5f, 0.3f, 3, 15).OnComplete(delegate {

                        objectTransform.DOShakeScale(0.3f, 0.3f, 3, 15).OnComplete(delegate
                        {

                            gaming.FinishGame();
                        });
                    });
                });

            }
            else
            {
                NextItem();
            }
                
        }

        protected override void NextItem()
        {
            base.NextItem();

            dropItems[index].raycastTarget = true;
        }


        public override void OnReset()
        {
            base.OnReset();

            if (Common.GameManager.Instance.setting.IsSound)
                audioSource.Play();

            index = 0;


            objectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(delegate {

                text0Transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(delegate {

                    text1Transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(delegate {

                        box0Transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
                        box1Transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

                    });
                });
	        });


            foreach (Image image in dropItems)
                image.raycastTarget = false;

            dropItems[index].raycastTarget = true;
        }

    }

}

