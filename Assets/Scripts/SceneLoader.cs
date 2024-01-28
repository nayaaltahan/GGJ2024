using System;
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
        
        public void OnPlay()
        {
            MainMenuCamera.gameObject.SetActive(false);
            SceneManager.LoadScene("Level", LoadSceneMode.Additive);
            SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
            Time.timeScale = 1;
            MainMenu.gameObject.SetActive(false);
        }

        public void OnGameOver()
        {
            GameOverMenu.gameObject.SetActive(true);
            GameOverText.text = $"You Pulled {NPCDeathCounterUI.NPCDeathCounter} Pranks!";
            Time.timeScale = 0;
        }
        
        public async void OnRestart()
        {
            GameOverMenu.gameObject.SetActive(false);
            MainMenu.gameObject.SetActive(true);
            await SceneManager.UnloadSceneAsync("Level");
            await SceneManager.UnloadSceneAsync("MainScene");
            MainMenuCamera.gameObject.SetActive(true);
        }

    }
}