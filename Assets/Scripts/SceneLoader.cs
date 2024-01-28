using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SceneLoader : MonoBehaviour
    {
        public CanvasGroup MainMenu;
        public CanvasGroup GameOverMenu;
        
        private void Awake()
        {
            MainMenu.alpha = 1;
        }
        
        public void OnPlay()
        {
            SceneManager.LoadScene("Level", LoadSceneMode.Additive);
            SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
            Time.timeScale = 1;
            MainMenu.alpha = 0;
        }

        public void OnGameOver()
        {
            GameOverMenu.alpha = 1;
            Time.timeScale = 0;
        }
        
        public async void OnRestart()
        {
            GameOverMenu.alpha = 0;
            MainMenu.alpha = 1;
            await SceneManager.UnloadSceneAsync("Level");
            await SceneManager.UnloadSceneAsync("MainScene");
        }

    }
}