using System;
using Game.Entities.Player;
using Game.Managers.GameManager;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.UI
{
    public class GamePauseUIComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject gamePauseUi;

        [SerializeField]
        private ScoreComponent scoreComponent;

        [SerializeField]
        private Slider musicVolumeSlider;
        
        private void Awake()
        {
            Assert.IsNotNull(scoreComponent, $"{nameof(scoreComponent)} must be set");
            Assert.IsNotNull(musicVolumeSlider, $"{nameof(musicVolumeSlider)} must be set");
            Assert.IsNotNull(gamePauseUi, $"{nameof(gamePauseUi)} must be set");
        }

        public void SetPlayer(PlayerData playerData)
        {
            scoreComponent.ObserveScore(playerData.Score);
        }
        
        private void OnEnable()
        {
            musicVolumeSlider.value = GameSingleton.Instance.PreferencesManager.MusicVolume;
            musicVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            GameSingleton.Instance.InputManger.PlayerControls.Main.Pause.performed += HandlePausePerformed;
        }

        private void OnDisable()
        {
            musicVolumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
            GameSingleton.Instance.InputManger.PlayerControls.Main.Pause.performed -= HandlePausePerformed;
        }
        
        private void HandlePausePerformed(InputAction.CallbackContext obj)
        {
            gamePauseUi.SetActive(!gamePauseUi.activeInHierarchy);
            GameSingleton.Instance.PauseManager.SetPaused(gamePauseUi.activeInHierarchy);
        }
        
        public void OnVolumeChanged(Single volume)
        {
            GameSingleton.Instance.MusicManager.Volume = volume;
            GameSingleton.Instance.PreferencesManager.MusicVolume = volume;
        }
        
        public void OnContinuePressed()
        {   
            gamePauseUi.SetActive(false);
            GameSingleton.Instance.PauseManager.SetPaused(false);
        }

        public void OnStartOverPressed()
        {
            GameSingleton.Instance.LevelManager.ReloadLevel();   
        }
    }
}