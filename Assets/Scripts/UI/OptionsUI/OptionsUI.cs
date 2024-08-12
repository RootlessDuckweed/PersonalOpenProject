using System;
using SaveAndLoad;
using UnityEngine;
using UnityEngine.UI;

namespace UI.OptionsUI
{
    public class OptionsUI : MonoBehaviour
    {
        [SerializeField] private Button quitAndSaveButton;
        private void Awake()
        {
            quitAndSaveButton.onClick.AddListener(QuitAndSave);
        }

        private void QuitAndSave()
        {
            Application.Quit();
        }
    }
}