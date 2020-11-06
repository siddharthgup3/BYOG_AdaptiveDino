using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BreakYourOwnGame.MainMenu
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void LoadGame()
        {
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}