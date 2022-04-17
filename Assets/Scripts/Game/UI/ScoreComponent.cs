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
        private void Awake()
        {
            Assert.IsNotNull(playerState, $"{nameof(playerState)} must be set");
            
            scoreText = GetComponent<TMP_Text>();
            Assert.IsNotNull(scoreText, $"{nameof(scoreText)} must be set");
        }

        private void OnEnable()
        {
            if (playerState != null)
            {
                playerState.playerData.Score.OnValueChanged += HandleScoreChanged;
            }
        }
        
        private void OnDisable()
        {
            if (playerState != null)
            {
                playerState.playerData.Score.OnValueChanged -= HandleScoreChanged;
            }
        }

        private void HandleScoreChanged(int score)
        {
            scoreText.text = $"{score:D}";
        }
    }
}