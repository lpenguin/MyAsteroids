using System;
using Game.GameManager;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.UI
{
    public class ScoreComponent: MonoBehaviour
    {
        [SerializeField]
        private PlayerState playerState;

        private TMP_Text scoreText;
        private void Start()
        {
            Assert.IsNotNull(playerState, $"{nameof(playerState)} must be set");
            
            scoreText = GetComponent<TMP_Text>();
            Assert.IsNotNull(scoreText, $"{nameof(scoreText)} must be set");
        }

        private void Update()
        {
            if (scoreText != null &&
                playerState != null)
            {
                scoreText.text = $"{playerState.playerData.Score:D}";
            }
        }
    }
}