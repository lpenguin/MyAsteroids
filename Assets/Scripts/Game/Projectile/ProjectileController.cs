using Game.Physics;
using UnityEngine;

namespace Game.Projectile
{
    public class ProjectileController: GameController
    {

        private readonly PhysicsBody2D _body2D;
        private readonly ProjectileDefinition _parameters;
        private readonly IGameComponent _gameComponent;
        private float _elapsedTime;
        
        public ProjectileController(PhysicsBody2D body2D, ProjectileDefinition parameters, IGameComponent gameComponent)
        {
            _body2D = body2D;
            _parameters = parameters;
            _gameComponent = gameComponent;
            _body2D.Velocity = gameComponent.Transform.up * parameters.speed;
            // TODO:
            _body2D.OnCollision += OnCollision;
        }

        public override void OnEnable()
        {
            _body2D.OnCollision += OnCollision;
        }

        public override void OnDisable()
        {
            _body2D.OnCollision -= OnCollision;
        }

        public override void Update(float timeStep)
        {
            _elapsedTime += timeStep;
            if (_elapsedTime > _parameters.lifeTime)
            {
                _gameComponent.DestroyGameObject();
            }
        }

        private void OnCollision(Collider2D other)
        {
            _gameComponent.DestroyGameObject();
            if (other.TryGetComponent<IHitReceiver>(out var hitReceiver))
            {
                hitReceiver.ReceiveHit(1);
            }
        }
    }
}