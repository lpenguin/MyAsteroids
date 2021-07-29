using Game.Physics;
using Game.Utils;
using UnityEngine;

namespace Game.Asteroid
{
    public class AsteroidController: IGameController
    {
        private readonly PhysicsBody2D _body2D;
        private readonly ScreenTunnelLogic _tunnelLogic;
        private readonly AsteroidDefinition _parameters;
        private readonly IGameView _gameView;

        public AsteroidController(PhysicsBody2D body2D, ScreenTunnelLogic tunnelLogic, AsteroidDefinition parameters, IGameView gameView)
        {
            _body2D = body2D;
            _tunnelLogic = tunnelLogic;
            _parameters = parameters;
            _gameView = gameView;
            _body2D.OnCollision += OnBodyCollision;
        }

        private void OnBodyCollision(Collider2D collider)
        {
            // Debug.Log($"Collision: {collider.gameObject}");
            ReceiveHit();
        }

        public void Update(float timeStep)
        {
            _tunnelLogic.UpdateTunnel();
        }

        public void PhysicsUpdate(float timeStep)
        {
            _body2D.Step(timeStep);
        }

        public void ReceiveHit()
        {
            if (_parameters.spawnOnDamage != null)
            {
                Vector2 initialPosition = _gameView.Transform.position;
                
                for (int i = 0; i < _parameters.spawnCount; i++)
                {
                    // TODO: duplicated code. Move to FloatRange extension
                    var offset = _parameters.spawnDistance.RandomVector2();

                    var go = Object.Instantiate(_parameters.spawnOnDamage, initialPosition + offset,
                        Quaternion.identity);
                }
            }
            _gameView.DestroyGameObject();
        }
    }
}