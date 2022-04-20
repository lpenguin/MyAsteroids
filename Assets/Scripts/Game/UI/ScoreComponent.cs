using Game.Entities.Player;
using Game.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.UI
{
    public class ScoreComponent: MonoBehaviour
    {
        private TMP_Text scoreText;
        private ObservableInt _score;

        public void ObserveScore(ObservableInt score)
        {
            _score = score;
            _score.OnValueChanged += HandleScoreChanged;
            HandleScoreChanged(0);
        }
        
        private void Awake()
        {
            scoreText = GetComponent<TMP_Text>();
            Assert.IsNotNull(scoreText, $"{nameof(TMP_Text)} must be set");
        }
        
        private void OnEnable()
        {
            if(_score == null) return;
            
            _score.OnValueChanged += HandleScoreChanged;
        }
        
        private void OnDisable()
        {
            if(_score == null) return;
            
            _score.OnValueChanged -= HandleScoreChanged;
        }

        private void HandleScoreChanged(int score)
        {
            if (scoreText == null) return;
            
            scoreText.text = $"{score:D}";
        }
    }
}