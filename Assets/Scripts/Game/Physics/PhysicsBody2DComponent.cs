using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Physics
{
    [AddComponentMenu("MyAsteroids/PhysicsBody2DComponent")]
    public class PhysicsBody2DComponent: MonoBehaviour
    {
        [SerializeField]
        private PhysicsBody2DDefinition definition;
        
        private PhysicsBody2D _body2D;
        
        public PhysicsBody2D Body2D => _body2D;
        
        private void Awake()
        {
            Assert.IsNotNull(definition);
            
            var shape = GetBodyShape();
            
            _body2D = new PhysicsBody2D(transform, definition, shape);
        }

        private IPhysicsBody2DShape GetBodyShape()
        {
            IPhysicsBody2DShape shape = null;
            if (TryGetComponent<CircleCollider2D>(out var circleCollider))
            {
                shape = new CirclePhysicsBody2DShape(circleCollider);
            }

            return shape;
        }

        private void FixedUpdate()
        {
            _body2D.Step(Time.fixedDeltaTime);
        }

        private void OnDrawGizmos()
        {
            if (_body2D == null)
            {
                return;
            }
            Gizmos.color = Color.green;
            var position = transform.position;
            Gizmos.DrawRay(position, _body2D.Velocity);
            
            Gizmos.color = Color.white;
            Gizmos.DrawRay(position, transform.up);
        }
    }
}