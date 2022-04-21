using Game.Entities.HitReceiver;
using Game.Events;
using Game.Managers.GameManager;
using Game.Utils;
using UnityEngine;

namespace Game.Entities.Asteroid
{
    public class AsteroidController
    {
        private readonly AsteroidDefinition _definition;
        private readonly AsteroidView _asteroidView;

        public AsteroidController(AsteroidView asteroidView, AsteroidDefinition definition, Rigidbody2D body2D)
        {
            body2D.velocity = definition.linearVelocityRange.RandomVector2();
            body2D.angularVelocity = definition.angularVelocityRange.RandomFloat();
            
            _definition = definition;
            _asteroidView = asteroidView;
        }


        public void HandleCollision(Collider2D collider)
        {
            if (collider.TryGetComponent<IHitReceiver>(out var hitReceiver))
            {
                hitReceiver.ReceiveHit(new HitData
                {
                    Damage = _definition.damage,
                });
            }
        }

        public void ReceiveHit(HitData hitData)
        {
            if (_definition.spawnOnDamage != null 
                && hitData.DamageType != DamageType.PreventSpawning)
            {
                SpawnAsteroids();
            }
            
            
            GameSingleton.Instance.EventBus.PublishEvent(new PlayerScoredEvent(_definition.score));
            _asteroidView.Destroy();
        }

        private void SpawnAsteroids()
        {
            Vector2 initialPosition = _asteroidView.Transform.position;

            for (int i = 0; i < _definition.spawnCount; i++)
            {
                var offset = _definition.spawnDistance.RandomVector2();

                // TODO: pooling
                Object.Instantiate(_definition.spawnOnDamage,
                    initialPosition + offset,
                    Quaternion.identity, _asteroidView.Transform.parent
                );
            }
        }
    }
}