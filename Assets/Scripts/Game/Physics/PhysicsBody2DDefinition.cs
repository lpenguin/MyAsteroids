using UnityEngine;

namespace Game.Physics
{
    [CreateAssetMenu(menuName = "MyAsteroids/PhysicsBody2D Definition")]
    public class PhysicsBody2DDefinition: ScriptableObject
    {
        public float linearDrag;

        public LayerMask collideMask = ~0;
        
        public float maxVelocity = float.MaxValue;
    }
}