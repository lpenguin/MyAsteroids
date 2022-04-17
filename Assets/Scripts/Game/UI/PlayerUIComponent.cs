using System;
using System.Globalization;
using Game.Events;
using Game.GameManager;
using Game.Player;
using Game.Utils;
using Game.Weapon;
using Game.Weapon.Laser;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerUIComponent: MonoBehaviour
    {
        [SerializeField] 
        private PlayerState playerState;
        
        [SerializeField]
        private Image laserChargeImage;
        
        [SerializeField]
        private Image laserChargeMaxImage;

        [SerializeField]
        private Image healthImage;

        [SerializeField]
        private Text scoreText;

        private LaserWeapon _laserWeapon;
        private PlayerController _playerController;
        
        private void OnEnable()
        {
            HandleScoreChanged(playerState.playerData.Score.Value);
            playerState.playerData.Score.OnValueChanged += HandleScoreChanged;
            playerState.eventBus.Subscribe<PlayerShipAddedEvent>(HandlePlayerShipAdded);
            playerState.eventBus.Subscribe<PlayerShipWeaponAddedEvent>(HandlePlayerShipWeaponAdded);
        }

        private void OnDisable()
        {
            playerState.playerData.Score.OnValueChanged -= HandleScoreChanged;
            playerState.eventBus.Unsubscribe<PlayerShipAddedEvent>(HandlePlayerShipAdded);
            playerState.eventBus.Unsubscribe<PlayerShipWeaponAddedEvent>(HandlePlayerShipWeaponAdded);
            
            if (_laserWeapon != null)
            {
                _laserWeapon.Charge.OnValueChanged -= HandleLaserChargeChanged;
            }
            
            if (_playerController != null)
            {
                _playerController.Health.OnValueChanged -= HandlePlayerHealthChanged;
            }
        }

        private void HandlePlayerShipAdded(PlayerShipAddedEvent playerShipAddedEvent)
        {
            _playerController = playerShipAddedEvent.PlayerController;
            _playerController.Health.OnValueChanged += HandlePlayerHealthChanged;
        }

        private void HandlePlayerShipWeaponAdded(PlayerShipWeaponAddedEvent playerShipWeaponAddedEvent)
        {
            if (playerShipWeaponAddedEvent.Weapon is not LaserWeapon weapon)
                return;

            _laserWeapon = weapon;
            _laserWeapon.Charge.OnValueChanged += HandleLaserChargeChanged;
        }
        
        private void HandleScoreChanged(int value)
        {
            scoreText.text = $"{value:D}";
        }

        private void HandlePlayerHealthChanged(float value)
        {
            healthImage.fillAmount = value;
        }
        
        private void HandleLaserChargeChanged(float value)
        {
            laserChargeImage.fillAmount = value;
            laserChargeMaxImage.enabled = Math.Abs(value - 1.0f) < 0.001;
        }
        
    }
}