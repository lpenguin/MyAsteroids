using System;
using Game.Entities.Player;
using Game.Events;
using Game.Managers.GameManager;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.UI
{
    public class GameOverUiComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject gameOverUi;

        [SerializeField]
        private ScoreComponent scoreComponent;
        
        private void Awake()
        {
            Assert.IsNotNull(gameOverUi, $"{nameof(gameOverUi)} must be set");
            Assert.IsNotNull(scoreComponent, $"{nameof(scoreComponent)} must be set");
        }

        private void OnEnable()
        {
            GameSingleton.Instance.EventBus.Subscribe<PlayerShipDestroyedEvent>(HandleGameOver);
        }

        private void OnDisable()
        {
            GameSingleton.Instance.EventBus.Unsubscribe<PlayerShipDestroyedEvent>(HandleGameOver);
        }
        
        public void SetPlayer(PlayerData playerData)
        {
            scoreComponent.ObserveScore(playerData.Score);
        }

        private void HandleGameOver(PlayerShipDestroyedEvent _)
        {
            gameOverUi.SetActive(true);
            GameSingleton.Instance.PauseManager.SetPaused(true);
        }

        public void OnStartOverPressed()
        {
            GameSingleton.Instance.LevelManager.ReloadLevel();   
        }
    }
}