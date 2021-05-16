using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Common
{
    public class ButtonsController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += delegate (UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1) {

                foreach (Button b in Resources.FindObjectsOfTypeAll<Button>())
                {
                    if(b.CompareTag("Button"))
                    {
                        b.onClick.AddListener(delegate
                        {
                            if (GameManager.Instance.setting.IsSound)
                            {
                                if (audioSource != null)
                                    audioSource.Play();
                            }

                            b.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).From(new Vector3(0.8f, 0.8f, 0.8f));
                        });
                    }

                }
            };


        }


    }


}


