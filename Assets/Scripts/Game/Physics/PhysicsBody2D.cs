using UnityEngine;

namespace Game.Physics
{
    public class PhysicsBody2D
    {
        
        public Vector2 Velocity { get; set; }
        public float AngularVelocity { get; set; }
        public Vector2 Acceleration { get; set; }

        
        public delegate void Collision(Collider2D collider);

        public event Collision OnCollision;
        

        private readonly Transform _transform;
        private readonly PhysicsBody2DDefinition _parameters;
        private readonly IPhysicsBody2DShape _shape;

        public PhysicsBody2D(Transform transform, PhysicsBody2DDefinition parameters, IPhysicsBody2DShape shape)
        {
            _transform = transform;
            _parameters = parameters;
            _shape = shape;
        }

        public void Step(float timeStep)
        {
            Velocity += (Acceleration - _parameters.linearDrag * Velocity) * timeStep;
            
            float speed = Velocity.magnitude;
            if (speed >= _parameters.maxVelocity)
            {
                Velocity *= _parameters.maxVelocity/speed;
            }

            Vector2 delta = Velocity * timeStep;

            if (OnCollision != null && _shape != null)
            {
                // TODO: use non-alloc method
                // TODO: extract function
                var colliders = _shape.Cast(
                    _transform.position,
                    delta.normalized,
                    delta.magnitude,
                    _parameters.collideMask);
                
                foreach (var collider in colliders)
                {
                    OnCollision.Invoke(collider);
                }
            }
            
            _transform.position += (Vector3)(delta);
            
            _transform.Rotate(Vector3.forward, AngularVelocity * timeStep);
            
            
        }

        public void SetThrust(float accelerationInput)
        {
            Acceleration = _transform.up * accelerationInput;
        }
    }
}