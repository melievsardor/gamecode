
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Touch;

namespace AlphabetBook
{
    public class Menu : MonoBehaviour, IScenes
    {
        [SerializeField]
        private List<Transform> cloudTransforms;

        [SerializeField]
        private AudioSource audioSource = null;

        [SerializeField]
        private AudioClip audioClip = null;

        [SerializeField]
        private Map map = null;

        [SerializeField]
        private TopBarScene topBarScene = null;


        [SerializeField]
        private LeanPitchYaw leanPitchYaw = null;

        [SerializeField]
        private LeanFingerTap leanFingerTap = null;

        [SerializeField]
        private LeanFingerSwipe leanFingerSwipe = null;

        private SnapScrolling snapScrolling;

        private void Start()
        {
            GameManager.Instance.ResetCamera();

            Constants.tempLevelIndex = GameManager.Instance.alphabetStats.Level;

            snapScrolling = GetComponentInChildren<SnapScrolling>();

            CloudTransforms();
        }

        private void CloudTransforms()
        {
            // back
            cloudTransforms[0].DOMoveX(-14f, Random.Range(50, 80)).SetLoops(-1, LoopType.Yoyo);
            cloudTransforms[1].DOMoveX(14f, Random.Range(50, 80)).SetLoops(-1, LoopType.Yoyo);
            cloudTransforms[2].DOMoveX(-14f, Random.Range(50, 80)).SetLoops(-1, LoopType.Yoyo);
            cloudTransforms[3].DOMoveX(14f, Random.Range(50, 80)).SetLoops(-1, LoopType.Yoyo);
        }


        public void OnClickedHandler(int index)
        {

            if (snapScrolling.selectPanID != index)
                return;

            Constants.tempLevelIndex = index;
          //  GameManager.Instance.alphabetStats.Level = index;
            

            if(Common.GameManager.Instance.setting.IsSound)
            {
                audioSource.loop = false;
                audioSource.clip = audioClip;
                audioSource.Play();
            }

            //show map
            GameManager.Instance.ShowFade(this);

        }

        public void SelectItem()
        {
            snapScrolling.SelectPadId();
        }

        public void ActiveLeanTouch()
        {
            leanPitchYaw.enabled = true;
            leanFingerTap.enabled = true;
            leanFingerSwipe.enabled = true;
        }

        public void OnClickBack()
        {
            Debug.Log("menu back");
            Common.UIManager.Instance.ShowUI();
        }

        public void OnCompleted()
        {
            leanPitchYaw.enabled = false;
            leanFingerTap.enabled = false;
            leanFingerSwipe.enabled = false;

            GameManager.Instance.ShowMap(true);

            map.EnableButtons();
         //   map.camController.SetCameraPos(0f);

            topBarScene.OnClickCancel();
        }
    } // end class
}


