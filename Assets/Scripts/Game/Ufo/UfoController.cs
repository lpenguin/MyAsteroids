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
        }

        public void HandleCollision(Collider2D other)
        {
            _component.DestroyGameObject();
            if (other.TryGetComponent<IHitReceiver>(out var receiver))
            {
                receiver.ReceiveHit(_definition.damage);
            }
        }

        public void HitReceived()
        {
            _component.DestroyGameObject();
            _definition.playerState.playerData.Score += _definition.score;
        }
        
        public override void Update(float timeStep)
        {
            var player = _definition.playerState.playerTransform;
            var transform = _component.Transform;
            var dir = player.position - transform.position;

            
            float lookAngle = Vector2.SignedAngle(Vector2.up, dir);
            
            _body2D.MoveRotation(Mathf.LerpAngle(_body2D.rotation, lookAngle, _definition.lookSmoothness * timeStep));
            _body2D.velocity = Vector2.Lerp(_body2D.velocity, (dir.normalized) * _definition.speed, _definition.speedSmoothness * timeStep);
            
        }
    }
}