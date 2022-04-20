using Game.Entities.HitReceiver;
using Game.Managers.GameManager;
using UnityEngine;

namespace Game.Entities.Ufo
{
    public class UfoController
    {
        private readonly UfoView _view;
        private readonly UfoDefinition _definition;
        public UfoController(UfoView view, UfoDefinition definition)
        {
            _view = view;
            _definition = definition;
            _view.Body2D.angularVelocity = definition.rotationSpeed;
        }

        public void HandleCollision(Collider2D other)
        {
            if (other.TryGetComponent<IHitReceiver>(out var receiver))
            {
                receiver.ReceiveHit(new HitData
                {
                    Damage = _definition.damage,
                });
            }
        }

        public void ReceiveHit(HitData hitData)
        {
            _view.Destroy();
        }
        
        public void Update(float timeStep)
        {
            var player = GameSingleton.Instance.PlayerTransform;
            
            var transform = _view.Transform;
            var dir = player.position - transform.position;

            _view.Body2D.velocity = Vector2.Lerp(_view.Body2D.velocity, (dir.normalized) * _definition.speed, _definition.speedSmoothness * timeStep);
        }
    }
}