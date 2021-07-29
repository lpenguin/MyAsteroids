using Game.Physics;
using UnityEngine;

namespace Game.Projectile
{
    public class ProjectileController: IGameController
    {

        private readonly PhysicsBody2D _body2D;
        private readonly ProjectileDefinition _parameters;
        private readonly IGameView _gameView;
        private float _elapsedTime = 0;
        public ProjectileController(PhysicsBody2D body2D, ProjectileDefinition parameters, IGameView gameView)
        {
            _body2D = body2D;
            _parameters = parameters;
            _gameView = gameView;
            _body2D.Velocity = gameView.Transform.up * parameters.speed;
            _body2D.OnCollision += OnCollision;
        }

        public void Update(float timeStep)
        {
            _elapsedTime += timeStep;
            if (_elapsedTime > _parameters.lifeTime)
            {
                _gameView.DestroyGameObject();
            }
        }

        private void OnCollision(Collider2D other)
        {
            _gameView.DestroyGameObject();
            if (other.TryGetComponent<IHitReceiver>(out var hitReceiver))
            {
                hitReceiver.ReceiveHit();
            }
        }

        public void PhysicsUpdate(float timeStep)
        {
            _body2D.Step(timeStep);
        }
    }
}