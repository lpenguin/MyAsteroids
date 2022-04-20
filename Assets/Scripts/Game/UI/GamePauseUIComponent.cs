using System;
using Game.Entities.Player;
using Game.Managers.GameManager;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Game.UI
{
    public class GamePauseUIComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject gamePauseUi;

        [SerializeField]
        private ScoreComponent scoreComponent;

        private void Awake()
        {
            Assert.IsNotNull(scoreComponent, $"{nameof(scoreComponent)} must be set");
        }

        public void SetPlayer(PlayerData playerData)
        {
            scoreComponent.ObserveScore(playerData.Score);
        }
        
        private void OnEnable()
        {
            GameSingleton.Instance.InputManger.PlayerControls.Main.Pause.performed += HandlePausePerformed;
        }

        private void OnDisable()
        {
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