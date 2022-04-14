using System;
using System.Globalization;
using Game.GameManager;
using Game.Utils;
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
        

        private void Update()
        {
            laserChargeImage.fillAmount = playerState.playerData.LaserCharge;
            laserChargeMaxImage.enabled = Math.Abs(playerState.playerData.LaserCharge - 1.0f) < 0.001;
            healthImage.fillAmount = playerState.playerData.Health;
            scoreText.text = $"{playerState.playerData.Score:D}";
        }
    }
}