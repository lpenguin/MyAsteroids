using Game.Entities.HitReceiver;
using UnityEngine;

namespace Game.Entities.Projectile
{
    public class ProjectileController
    {

        private readonly ProjectileView _view;
        private readonly ProjectileModel _model;
        
        public ProjectileController(ProjectileView view, ProjectileDefinition definition,
            ProjectileModel model)
        {
            _view = view;
            _model = model;
            
            _view.Rigidbody2D.velocity = view.Transform.up * definition.speed;
        }
        

        public void HandleCollisionEnter(Collision2D col)
        {
            _view.Destroy();
            if (col.collider.TryGetComponent<IHitReceiver>(out var hitReceiver))
            {
                hitReceiver.ReceiveHit(_model.HitData);
            }
        }
    }
}