using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace Common
{
    public class LanguagePopup : MonoBehaviour
    {

        [SerializeField] private Button buttonRussian;
        [SerializeField] private Button buttonUzbek;

        private void Start()
        {
            buttonRussian.onClick.AddListener(() => HandlerButtonClicked());
            buttonUzbek.onClick.AddListener(() => HandlerButtonClicked());
        }


        private void HandlerButtonClicked()
        {
            //LocalizationManager.Instance.LoadLocalizedText(value);
            //GameManager.Instance.setting.Langeage = value;

            UIManager.Instance.ShowUI();
        }

    }

}

