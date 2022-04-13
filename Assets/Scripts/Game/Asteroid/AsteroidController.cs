using Game.GameManager;
using Game.Physics;
using Game.Utils;
using UnityEngine;

namespace Game.Asteroid
{
    public class AsteroidController: GameController
    {
        private readonly PhysicsBody2D _body2D;
        private readonly AsteroidDefinition _parameters;
        private readonly IGameComponent _gameComponent;

        public AsteroidController(IGameComponent gameComponent, AsteroidDefinition parameters, PhysicsBody2D body2D)
        {
            _body2D = body2D;
            _body2D.Velocity = parameters.linearVelocityRange.RandomVector2();
            _body2D.AngularVelocity = parameters.angularVelocityRange.RandomFloat();
            
            _parameters = parameters;
            _gameComponent = gameComponent;
            // TODO:
            _body2D.OnCollision += OnBodyCollision;
        }

        public override void OnEnable()
        {
            _body2D.OnCollision += OnBodyCollision;
        }

        public override void OnDisable()
        {
            _body2D.OnCollision -= OnBodyCollision;
        }

        private void OnBodyCollision(Collider2D collider)
        {
            // Debug.Log($"Collision: {collider.gameObject}");
            ReceiveHit();
            if (collider.TryGetComponent<IHitReceiver>(out var hitReceiver))
            {
                hitReceiver.ReceiveHit(_parameters.damage);
            }
        }

        public void ReceiveHit()
        {
            if (_parameters.spawnOnDamage != null)
            {
                Vector2 initialPosition = _gameComponent.Transform.position;
                
                for (int i = 0; i < _parameters.spawnCount; i++)
                {
                    // TODO: duplicated code. Move to FloatRange extension
                    var offset = _parameters.spawnDistance.RandomVector2();

                    var go = Object.Instantiate(_parameters.spawnOnDamage, initialPosition + offset,
                        Quaternion.identity, _gameComponent.Transform.parent);
                }
            }
            _gameComponent.DestroyGameObject();
            _parameters.playerState.score += _parameters.score;
        }
    }
}