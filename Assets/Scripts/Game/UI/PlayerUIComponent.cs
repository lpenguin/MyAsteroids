using System;
using Game.Entities.Player;
using Game.Entities.Weapon.Laser;
using Game.Events;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerUIComponent: MonoBehaviour, IPlayerWeaponsUI
    {
        [SerializeField]
        private Image laserChargeImage;
        
        [SerializeField]
        private Image laserChargeMaxImage;

        [SerializeField]
        private Image healthImage;

        [SerializeField]
        private ScoreComponent scoreComponent;

        private ILaserWeapon _laserWeapon;
        private PlayerData _playerData;

        public void SetPlayer(PlayerData playerData)
        {
            _playerData = playerData;
            scoreComponent.ObserveScore(playerData.Score);
            BindEvents();
        }

        public void SetupLaserWeapon(ILaserWeapon laserWeapon)
        {
            _laserWeapon = laserWeapon;
            _laserWeapon.Charge.OnValueChanged += HandleLaserChargeChanged;
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
            if (_playerData != null)
            {
                _playerData.Health.OnValueChanged += HandlePlayerHealthChanged;
            }

            if (_laserWeapon != null)
            {
                _laserWeapon.Charge.OnValueChanged += HandleLaserChargeChanged;
            }
        }
        
        private void UnbindEvents()
        {
            if (_playerData != null)
            {
                _playerData.Health.OnValueChanged -= HandlePlayerHealthChanged;
            }
            
            if (_laserWeapon != null)
            {
                _laserWeapon.Charge.OnValueChanged -= HandleLaserChargeChanged;
            }
        }

        private void HandlePlayerHealthChanged(float value)
        {
            healthImage.fillAmount = value;
        }
        
        private void HandleLaserChargeChanged(float value)
        {
            laserChargeImage.fillAmount = value;
            laserChargeMaxImage.enabled = value > .9f;
        }
    }
}