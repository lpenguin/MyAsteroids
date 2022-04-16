using UnityEngine;

namespace Game.Projectile
{
    public class ProjectileController: GameController
    {

        private readonly ProjectileDefinition _parameters;
        private readonly IGameComponent _gameComponent;
        private float _elapsedTime;
        
        public ProjectileController(ProjectileDefinition parameters, IGameComponent gameComponent, Rigidbody2D body2D)
        {
            _parameters = parameters;
            _gameComponent = gameComponent;
            body2D.velocity = gameComponent.Transform.up * parameters.speed;
        }
        

        public override void Update(float timeStep)
        {
            _elapsedTime += timeStep;
            if (_elapsedTime > _parameters.lifeTime)
            {
                _gameComponent.DestroyGameObject();
            }
        }

        public void HandleCollisionEnter(Collision2D col)
        {
            _gameComponent.DestroyGameObject();
        }
    }
}