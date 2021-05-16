
using System.Collections;
using UnityEngine;


namespace AlphabetBook
{
    public class QGame : TouchBase
    {
        [SerializeField]
        private AudioClip clip = null;

        protected override void Start()
        {
            base.Start();

            StartCoroutine(WaitAudio());
        }

        private IEnumerator WaitAudio()
        {
            yield return new WaitForSeconds(0.7f);

            if (Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }

        }


    }



}


