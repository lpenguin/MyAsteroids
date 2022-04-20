using Game.Entities.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.UI
{
    public class ScoreComponent: MonoBehaviour
    {
        private TMP_Text scoreText;
        private PlayerData _playerData;

        public void SetPlayer(PlayerData playerData)
        {
            _playerData = playerData;
        }
        
        private void Awake()
        {
            scoreText = GetComponent<TMP_Text>();
            Assert.IsNotNull(scoreText, $"{nameof(TMP_Text)} must be set");
        }
        
        private void OnEnable()
        {
            if(_playerData == null) return;
            
            _playerData.Score.OnValueChanged += HandleScoreChanged;
        }
        
        private void OnDisable()
        {
            if(_playerData == null) return;
            
            _playerData.Score.OnValueChanged += HandleScoreChanged;
        }

        private void HandleScoreChanged(int score)
        {
            scoreText.text = $"{score:D}";
        }
    }
}