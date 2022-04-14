using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Game.GameManager
{
    public class GameManager: MonoBehaviour
    {
        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private GameObject gameOverUi;

        [SerializeField]
        private TMPro.TMP_Text scoreText;

        private void Start()
        {
            gameOverUi.SetActive(false);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
        
        private void OnEnable()
        {
            Assert.IsNotNull(playerState, $"{nameof(playerState)} must be set");
            Assert.IsNotNull(gameOverUi, $"{nameof(gameOverUi)} must be set");
            Assert.IsNotNull(scoreText, $"{nameof(scoreText)} must be set");
            
            playerState.OnPlayerDeath += HandlePlayerDeath;
        }

        private void OnDisable()
        {
            playerState.OnPlayerDeath -= HandlePlayerDeath;
        }
        
        private void HandlePlayerDeath()
        {
            Time.timeScale = 0f;
            gameOverUi.SetActive(true);
            scoreText.text = $"{playerState.playerData.Score:D}";
        }

    }
}