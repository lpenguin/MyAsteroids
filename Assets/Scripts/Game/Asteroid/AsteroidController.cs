using Game.HitReceiver;
using Game.Utils;
using Game.Weapon.Laser;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Asteroid
{
    public class AsteroidController: GameController
    {
        private readonly AsteroidDefinition _definition;
        private readonly IGameComponent _gameComponent;

        public AsteroidController(IGameComponent gameComponent, AsteroidDefinition definition, Rigidbody2D body2D)
        {
            body2D.velocity = definition.linearVelocityRange.RandomVector2();
            body2D.angularVelocity = definition.angularVelocityRange.RandomFloat();
            
            _definition = definition;
            _gameComponent = gameComponent;
        }


        public void HandleCollision(Collider2D collider)
        {
            if (collider.TryGetComponent<IHitReceiver>(out var hitReceiver))
            {
                hitReceiver.ReceiveHit(new ReceiveHitData
                {
                    Damage = _definition.damage,
                });
            }
        }

        public void ReceiveHit(ReceiveHitData receiveHitData)
        {
            if (_definition.spawnOnDamage != null 
                && receiveHitData.Owner is not LaserWeapon)
            {
                SpawnAsteroids();
            }

            if (_definition.vfxPrefab != null)
            {
                Object.Instantiate(_definition.vfxPrefab, 
                    _gameComponent.Transform.position,
                    _gameComponent.Transform.rotation);
            }
            
            if (receiveHitData.PlayerData != null)
            {
                receiveHitData.PlayerData.Score += _definition.score;    
            }
            _gameComponent.DestroyGameObject();
        }

        private void SpawnAsteroids()
        {
            Vector2 initialPosition = _gameComponent.Transform.position;

            for (int i = 0; i < _definition.spawnCount; i++)
            {
                var offset = _definition.spawnDistance.RandomVector2();

                Object.Instantiate(_definition.spawnOnDamage, initialPosition + offset,
                    Quaternion.identity, _gameComponent.Transform.parent);
            }
        }
    }
}