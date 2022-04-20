using Game.Events;
using Game.Managers.GameManager;
using UnityEngine;

namespace Game.Entities.Ufo
{
    public class UfoView
    {
        public Transform Transform { get; }
        public Rigidbody2D Body2D { get; }
        
        private readonly UfoDefinition _definition;
        
        public UfoView(Rigidbody2D body2D, Transform transform, UfoDefinition definition)
        {
            _definition = definition;
            Transform = transform;
            Body2D = body2D;
        }

        public void Destroy()
        {
            if (_definition.destroyVfx != null)
            {
                _definition.destroyVfx.InstantiateAsync(
                    Transform.position,
                    Transform.rotation);
            }
            
            Object.Destroy(Transform.gameObject);
            
            GameSingleton.Instance.EventBus.PublishEvent(new PlayerScoredEvent(_definition.score));
        }
    }
}