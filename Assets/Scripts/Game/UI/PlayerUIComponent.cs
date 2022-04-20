using System;
using Game.Entities.Player;
using Game.Events;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game.UI
{
    // TODO: player laser charge controller
    // TODO: player health controller
    public class PlayerUIComponent: MonoBehaviour
    {
        [SerializeField]
        private Image laserChargeImage;
        
        [SerializeField]
        private Image laserChargeMaxImage;

        [SerializeField]
        private Image healthImage;

        [SerializeField]
        private ScoreComponent scoreComponent;

        // private LaserWeapon _laserWeapon;
        private PlayerData _playerData;

        public void SetPlayer(PlayerData playerData)
        {
            _playerData = playerData;
            scoreComponent.SetPlayer(playerData);
            BindEvents();
        }

        private void Awake()
        {
            Assert.IsNotNull(laserChargeImage, $"{nameof(laserChargeImage)} must be set");
            Assert.IsNotNull(laserChargeMaxImage, $"{nameof(laserChargeMaxImage)} must be set");
            Assert.IsNotNull(healthImage, $"{nameof(healthImage)} must be set");
            Assert.IsNotNull(scoreComponent, $"{nameof(scoreComponent)} must be set");
        }

        private void OnEnable()
        {
            BindEvents();
        }

        private void OnDisable()
        {
            UnbindEvents();
        }

        private void BindEvents()
        {
            if(_playerData == null) return;

            _playerData.Health.OnValueChanged += HandlePlayerHealthChanged;
        }
        
        private void UnbindEvents()
        {
            if(_playerData == null) return;
            
            _playerData.Health.OnValueChanged -= HandlePlayerHealthChanged;
        }


        private void HandlePlayerHealthChanged(float value)
        {
            healthImage.fillAmount = value;
        }
    }
}