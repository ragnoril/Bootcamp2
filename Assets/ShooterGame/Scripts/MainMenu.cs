using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShooterGame
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject CreditsPanel;
        public Button StartGameButton;
        public SceneLoader sceneLoader;

        private void Start()
        {

            StartGameButton.onClick.AddListener(() => StartGame());
        }

        public void StartGame()
        {
            //SceneManager.LoadScene("GameScene");
            sceneLoader.sceneIndexToLoad = 1;
            SceneManager.LoadScene(2);
        }

        public void OpenCredits()
        {
            CreditsPanel.SetActive(true);
        }

        public void CloseCredits()
        {
            CreditsPanel.SetActive(false);
        }

        public void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }

}