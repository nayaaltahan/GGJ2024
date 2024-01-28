using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SceneLoader : MonoBehaviour
    {
        public CanvasGroup MainMenu;
        public CanvasGroup GameOverMenu;
        public TMP_Text GameOverText;
        public Camera MainMenuCamera;

        private void Awake()
        {
            GameOverMenu.gameObject.SetActive(false);
            MainMenu.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
                OnGameOver();
            
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (Time.timeScale == 0)
                    Time.timeScale = 1;
                else
                    Time.timeScale = 0;
            }
            
            
        }

        public async void OnPlay()
        {
            MainMenuCamera.gameObject.SetActive(false);
            SceneManager.LoadScene("Level", LoadSceneMode.Additive);
            SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
            await UniTask.DelayFrame(1);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level"));
            Time.timeScale = 1;
            MainMenu.gameObject.SetActive(false);
        }

        public void OnGameOver()
        {
            GameOverMenu.gameObject.SetActive(true);
            GameOverText.text = $"{NPCDeathCounterUI.NPCDeathCounter} Pranks";
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

        public void OnRestart()
        {
            SceneManager.LoadScene(0);
            // StartCoroutine(RestartGameCoroutine());
        }

        private IEnumerator RestartGameCoroutine()
        {
            GameOverMenu.gameObject.SetActive(false);
            MainMenu.gameObject.SetActive(true);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menus"));
            yield return new WaitForEndOfFrame();
            var mainScene = SceneManager.UnloadSceneAsync(1);
            var unloadLevel = SceneManager.UnloadSceneAsync(2);
            yield return unloadLevel;
            yield return mainScene;
            yield return new WaitForEndOfFrame();
            MainMenuCamera.gameObject.SetActive(true);
        }
        
        public void OnQuit()
        {
            Application.Quit();
        }
    }
}