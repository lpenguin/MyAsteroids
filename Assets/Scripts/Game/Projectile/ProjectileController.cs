using Game.HitReceiver;
using UnityEngine;

namespace Game.Projectile
{
    public class ProjectileController: GameController
    {

        private readonly IGameComponent _gameComponent;
        private readonly ReceiveHitData _receiveHitData;
        
        public ProjectileController(
            ProjectileDefinition definition,
            ReceiveHitData receiveHitData,
            IGameComponent gameComponent, 
            Rigidbody2D body2D)
        {
            _gameComponent = gameComponent;
            _receiveHitData = receiveHitData;
            body2D.velocity = gameComponent.Transform.up * definition.speed;
        }
        

        public void HandleCollisionEnter(Collision2D col)
        {
            _gameComponent.DestroyGameObject();
            if (col.collider.TryGetComponent<IHitReceiver>(out var hitReceiver))
            {
                hitReceiver.ReceiveHit(_receiveHitData);
            }
        }
    }
}