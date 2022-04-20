using Game.Events;
using Game.Managers.GameManager;

namespace Game.Entities.Player
{
    public class PlayerScoreController
    {
        private readonly PlayerData _playerData;

        public PlayerScoreController(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void BindEvents()
        {
            GameSingleton.Instance.EventBus.Subscribe<PlayerScoredEvent>(HandlePlayerScored);
        }


        public void UnbindEvents()
        {
            GameSingleton.Instance.EventBus.Unsubscribe<PlayerScoredEvent>(HandlePlayerScored);
        }
        
        private void HandlePlayerScored(PlayerScoredEvent evt)
        {
            _playerData.Score.Value += evt.Score;
        }
    }
}