using UnityEngine;

namespace Game.Ufo
{
    public class UfoController: GameController
    {
        private readonly IGameComponent _component;
        private readonly UfoDefinition _definition;
        private readonly Rigidbody2D _body2D;

        public UfoController(IGameComponent component, UfoDefinition definition, Rigidbody2D body2D)
        {
            _component = component;
            _definition = definition;
            _body2D = body2D;
            _body2D.angularVelocity = definition.rotationSpeed;
        }

        public void HandleCollision(Collider2D other)
        {
            ReceiveHit();
            if (other.TryGetComponent<IHitReceiver>(out var receiver))
            {
                receiver.ReceiveHit(_definition.damage);
            }
        }

        public void ReceiveHit()
        {
            if (_definition.vfxPrefab != null)
            {
                Object.Instantiate(_definition.vfxPrefab, 
                    _component.Transform.position,
                    _component.Transform.rotation);
            }
            
            _component.DestroyGameObject();
            _definition.playerState.playerData.Score += _definition.score;
        }
        
        public override void Update(float timeStep)
        {
            var player = _definition.playerState.playerTransform;
            var transform = _component.Transform;
            var dir = player.position - transform.position;

            _body2D.velocity = Vector2.Lerp(_body2D.velocity, (dir.normalized) * _definition.speed, _definition.speedSmoothness * timeStep);
            
        }
    }
}