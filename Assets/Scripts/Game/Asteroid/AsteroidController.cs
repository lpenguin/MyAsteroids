using Game.Utils;
using UnityEngine;

namespace Game.Asteroid
{
    public class AsteroidController: GameController
    {
        private readonly Rigidbody2D _body2D;
        private readonly AsteroidDefinition _parameters;
        private readonly IGameComponent _gameComponent;

        public AsteroidController(IGameComponent gameComponent, AsteroidDefinition parameters, Rigidbody2D body2D)
        {
            _body2D = body2D;
            _body2D.velocity = parameters.linearVelocityRange.RandomVector2();
            _body2D.angularVelocity = parameters.angularVelocityRange.RandomFloat();
            
            _parameters = parameters;
            _gameComponent = gameComponent;
        }


        public void HandleCollision(Collider2D collider)
        {
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