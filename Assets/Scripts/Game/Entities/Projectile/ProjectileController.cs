using Game.Entities.HitReceiver;
using UnityEngine;

namespace Game.Entities.Projectile
{
    public class ProjectileController
    {

        private readonly ProjectileView _view;
        private readonly HitData _hitData;
        
        public ProjectileController(ProjectileView view, ProjectileDefinition definition,
            HitData hitData)
        {
            _view = view;
            _hitData = hitData;
            
            _view.Rigidbody2D.velocity = view.Transform.up * definition.speed;
        }
        

        public void HandleCollisionEnter(Collision2D col)
        {
            _view.Destroy();
            if (col.collider.TryGetComponent<IHitReceiver>(out var hitReceiver))
            {
                hitReceiver.ReceiveHit(_hitData);
            }
        }
    }
}