using System;
using Game.Events;
using Game.Input;
using Game.Music;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace Game.GameManager
{
    // TODO: Singleton?
    public class GameManager: MonoBehaviour
    {
        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private GameObject gameOverUi;

        [SerializeField]
        private GameObject pauseUi;

        private bool _isPaused;

        private void Awake()
        {
            // TODO: use ServiceLocator pattern
            playerState.eventBus = new EventBus();
        }

        private void Start()
        {
            gameOverUi.SetActive(false);
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            pauseUi.SetActive(true);
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            pauseUi.SetActive(false);
        }
        
        public void RestartLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
        
        public void Exit()
        {
            Application.Quit();
        }

        public void ChangeVolume(Single volume)
        {
            MusicManager.Instance.Volume = volume;
        }

        private void OnEnable()
        {
            playerState.audioListener = FindObjectOfType<AudioListener>();
            
            Assert.IsNotNull(playerState, $"{nameof(playerState)} must be set");
            Assert.IsNotNull(gameOverUi, $"{nameof(gameOverUi)} must be set");
            Assert.IsNotNull(playerState.audioListener, $"Cannot find {nameof(AudioListener)} in the scene");

            
            playerState.eventBus.Subscribe<PlayerShipDestroyedEvent>(HandlePlayerDeath);
            InputManager.Instance.Controls.Main.Pause.performed += OnPausePresed;
        }

        private void OnPausePresed(InputAction.CallbackContext obj)
        {
            _isPaused = !_isPaused;
            if (_isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }

        private void OnDisable()
        {
            InputManager.Instance.Controls.Main.Pause.performed -= OnPausePresed;
            playerState.eventBus.Unsubscribe<PlayerShipDestroyedEvent>(HandlePlayerDeath);
        }
        
        private void HandlePlayerDeath(PlayerShipDestroyedEvent _)
        {
            Time.timeScale = 0f;
            gameOverUi.SetActive(true);
        }

    }
}