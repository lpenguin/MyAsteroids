using System;
using Game.Physics;
using UnityEngine;

namespace Game.Ufo
{
    public class UfoController: GameController
    {
        private readonly IGameComponent _component;
        private readonly UfoDefinition _definition;
        private readonly PhysicsBody2D _body2D;

        public UfoController(IGameComponent component, UfoDefinition definition, PhysicsBody2D body2D)
        {
            _component = component;
            _definition = definition;
            _body2D = body2D;
            _body2D.OnCollision += OnCollision;
        }

        private void OnCollision(Collider2D other)
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
            _definition.playerState.score += _definition.score;
        }
        
        public override void Update(float timeStep)
        {
            var player = _definition.playerState.playerTransform;
            var transform = _component.Transform;
            var dir = player.position - transform.position;

            
            float lookAngle = Vector2.SignedAngle(Vector2.up, dir);
            
            // _body2D.Rotation = lookAngle;
            _body2D.Rotation = Mathf.LerpAngle(_body2D.Rotation, lookAngle, _definition.lookSmoothness * timeStep);
            _body2D.Velocity = Vector2.Lerp(_body2D.Velocity, (dir.normalized) * _definition.speed, _definition.speedSmoothness * timeStep);
            
        }
    }
}