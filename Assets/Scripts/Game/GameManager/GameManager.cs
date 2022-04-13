using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.GameManager
{
    public class GameManager: MonoBehaviour
    {
        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private GameObject gameOverUi;
        
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
        
        private void OnEnable()
        {
            Assert.IsNotNull(playerState);
            Assert.IsNotNull(gameOverUi);
            
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
        }

    }
}