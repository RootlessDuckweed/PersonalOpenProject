using System;
using System.Collections;
using Enemy.Enemy_Skeleton;
using Inventory;
using Player.Universal;
using SaveAndLoad;
using SceneManager;
using UnityEngine;
using UnityEngine.UI;

//do Scene transition function
namespace UI.MainMenuUI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private string firstScene;
        [SerializeField] private Button continueButton;
        [SerializeField] private PlayerController player;
        private void Start()
        {
            continueButton.gameObject.SetActive(SaveManager.Instance.HasSavedData());
        }

        public void ContinueGame()
        {

            StartCoroutine(SceneLoaderManager.Instance.LoadSceneWithFadeEffectContinueGame(1f,firstScene));
        }

        private void Update()
        {
            player.playerInput.Disable();
        }

        public void NewGame()
        {
            SaveManager.Instance.DeleteTheData();
            StartCoroutine(SceneLoaderManager.Instance.LoadSceneWithFadeEffectNewGame(1f,firstScene));
        }

        public void ExitGame()
        {
            Application.Quit();
        }
        
        
    }
}