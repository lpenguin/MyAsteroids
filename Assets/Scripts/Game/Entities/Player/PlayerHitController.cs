using Game.Entities.HitReceiver;
using Game.Events;
using Game.Managers.GameManager;
using UnityEngine;

namespace Game.Entities.Player
{
    public class PlayerHitController
    {
        private readonly PlayerView _playerView;
        private readonly PlayerData _playerData;
        private readonly PlayerDefinition _definition;

        public PlayerHitController(PlayerView playerView, PlayerData playerData, PlayerDefinition definition)
        {
            _playerView = playerView;
            _playerData = playerData;
            _definition = definition;
        }
        
        public void ReceiveHit(HitData hitData)
        {
            _playerData.Health.Value = Mathf.Max(0, _playerData.Health.Value - hitData.Damage);
            
            if (_playerData.Health.Value == 0)
            {
                GameSingleton.Instance.EventBus.PublishEvent(new PlayerShipDestroyedEvent());
                _playerView.ShowPlayerDeath();
            }
        }
        
        public void HandleCollisionEnter(Collision2D col)
        {
            if (col.collider.TryGetComponent<IHitReceiver>(out var hitReceiver))
            {
                hitReceiver.ReceiveHit(new HitData
                {
                    Damage = _definition.collisionDamage
                });
            }
        }
    }
}