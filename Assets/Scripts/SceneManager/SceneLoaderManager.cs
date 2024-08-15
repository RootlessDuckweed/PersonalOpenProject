using System.Collections;
using Inventory;
using SaveAndLoad;
using UI.MainMenuUI;
using UnityEngine;
using Utility;

namespace SceneManager
{
    public class SceneLoaderManager : Singleton<SceneLoaderManager>
    {
        [SerializeField] private string mainMenu = "MainMenu";
        [SerializeField] private string currentScene="";
        [SerializeField] private UI_FadeScreen fadeScreen;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);
            currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            //todo: test
            //InventoryManager.Instance.NewGameAddOriginalItem();
        }

        public IEnumerator LoadSceneWithFadeEffectNewGame(float fadeDuration,string scene)
        {
           
            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeDuration);
            var handle = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
            handle.completed += (obj) =>
            {
                InventoryManager.Instance.NewGameAddOriginalItem();
                fadeScreen.FadeIn();
                currentScene = scene;
            };
            yield return handle;
        }
        
        public IEnumerator LoadSceneWithFadeEffectContinueGame(float fadeDuration,string scene)
        {
            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeDuration);
            var handle = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
            handle.completed += (obj) =>
            {
                fadeScreen.FadeIn();
                currentScene = scene;
            };
            yield return handle;
        }
        
        public IEnumerator LoadSceneWithFadeEffectMainMenu(float fadeDuration)
        {
            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeDuration);
            SaveManager.Instance.SaveGame();
            var handle = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(mainMenu);
            handle.completed += (obj) =>
            {
                fadeScreen.FadeIn();
                currentScene = mainMenu;
            };
            yield return handle;
        }

        public IEnumerator ReStartTheGame()
        {
            SaveManager.Instance.SaveGame();
            // 开始淡出
            fadeScreen.FadeOut();

            // 等待淡出完成
            yield return new WaitForSeconds(1f);
            var handle =UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(currentScene);
            handle.completed += (obj) =>
            {
                fadeScreen.FadeIn();
            };
            yield return handle;
           
        }

        public void ReStart()
        {
            StartCoroutine(ReStartTheGame());
        }
        public void ComeBackMainMenu()
        {
            StartCoroutine(LoadSceneWithFadeEffectMainMenu(1f));
        }
        
    }
}