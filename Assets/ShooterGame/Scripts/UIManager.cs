using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace ShooterGame
{
    public class UIManager : MonoBehaviour
    {
        public PlayerController Player;

        public Image HealthBar;
        public Image HealthBarCursor;

        public TMP_Text PlayerScore;

        public GameObject GameOverMenu;
        public TMP_Text HighScore;
        public TMP_Text CurrentScore;

        private void Start()
        {
            UpdatePlayerHealth(0);
            UpdatePlayerScore();

            EventManager.Instance.OnEnemyKilled += UpdatePlayerScore;
            EventManager.Instance.OnPlayerHit += UpdatePlayerHealth;
            EventManager.Instance.OnGameOver += OpenGameOverMenu;
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnEnemyKilled -= UpdatePlayerScore;
            EventManager.Instance.OnPlayerHit -= UpdatePlayerHealth;
            EventManager.Instance.OnGameOver -= OpenGameOverMenu;
        }

        public void UpdatePlayerScore()
        {
            PlayerScore.text = "Score: " + Player.Score;
        }

        public void UpdatePlayerHealth(float damage)
        {
            float currentHealthWidth = (400 * Player.CurHealth) / Player.MaxHealth;
            HealthBarCursor.rectTransform.sizeDelta = new Vector2(currentHealthWidth, 60);
        }

        public void OpenGameOverMenu()
        {
            HighScore.text = "High Score: " + Player.HighScore;
            CurrentScore.text = "Current Score: " + Player.Score;
            GameOverMenu.SetActive(true);
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(1);
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }


    }
}
