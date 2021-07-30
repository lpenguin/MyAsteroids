﻿using UnityEngine;

namespace Game.Physics
{
    public class PhysicsBody2D
    {
        
        public Vector2 Velocity { get; set; }
        public float AngularVelocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Vector2 Position => _transform.position;

        public float Rotation
        {
            get => _transform.rotation.eulerAngles.z;
            set => _transform.rotation = Quaternion.AngleAxis(value, Vector3.forward);
        }

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
            Velocity = Vector2.ClampMagnitude(Velocity, _parameters.maxVelocity);

            Vector2 deltaPos = Velocity * timeStep;

            if (OnCollision != null && _shape != null)
            {
                // TODO: use non-alloc method
                // TODO: extract function
                var colliders = _shape.Cast(
                    _transform.position,
                    deltaPos.normalized,
                    deltaPos.magnitude,
                    _parameters.collideMask);
                
                foreach (var collider in colliders)
                {
                    OnCollision.Invoke(collider);
                }
            }
            
            _transform.position += (Vector3)deltaPos;
            _transform.Rotate(Vector3.forward, AngularVelocity * timeStep);
        }

        public void SetThrust(float accelerationInput)
        {
            Acceleration = _transform.up * accelerationInput;
        }
    }
}