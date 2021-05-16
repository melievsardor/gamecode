
using UnityEngine;


namespace AlphabetBook
{
    public class TopBarScene : MonoBehaviour
    {
        [SerializeField]
        private Common.Settings settings = null;

        [SerializeField]
        private Menu menu = null;


        [SerializeField]
        private MobileBlur mobileBlur = null;

        [SerializeField]
        private Camera settingCamera = null;


        private NextPupop nextPupop;

        private AudioSource audioSource;

        private bool isBlock;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            nextPupop = FindObjectOfType<NextPupop>();
        }


        public void OnClickSetting()
        {
            //if (isBlock)
            //    return;

            //isBlock = true;

            if(Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();
            }

            mobileBlur.enabled = true;

            settings.OnClickOpen();

            settingCamera.enabled = true;

        }

        public void OnClickCancel()
        {
            isBlock = false;

            if (Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();
            }

            settings.OnClickCancel();


            mobileBlur.enabled = false;

            settingCamera.enabled = false;
        }

        public void OnClickHome()
        {
            //if (isBlock)
            //    return;

            //isBlock = true;

            if (Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.Play();
            }

            nextPupop.Hide();

            if (menu.gameObject.activeSelf)
                menu.OnClickBack();
            else
                GameManager.Instance.OnClickHome();
        }
    }

}

