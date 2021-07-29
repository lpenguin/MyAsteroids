using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Physics
{
    public interface IPhysicsBody2DShape
    {
        IReadOnlyList<Collider2D> Cast(Vector2 position, Vector2 direction, float distance, LayerMask mask);
    }

    
    public class CirclePhysicsBody2DShape : IPhysicsBody2DShape
    {
        private readonly float _radius;
        // TODO: support not centered collider
        // Means we need to take into account the rotation
        
        public CirclePhysicsBody2DShape(CircleCollider2D collider)
        {
            _radius = collider.radius;
        }
        
        public IReadOnlyList<Collider2D> Cast(Vector2 position, Vector2 direction, float distance, LayerMask mask)
        {
            // TODO: not very performant code
            var hits = Physics2D.CircleCastAll(
                position,
                _radius, 
                direction, 
                distance,
                mask);
            return Array.ConvertAll(hits, hit => hit.collider);
        }
    }
}