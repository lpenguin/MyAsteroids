using UnityEngine;

namespace Game.Entities.Asteroid
{
    public class AsteroidView
    {
        private readonly AsteroidDefinition _definition;
        public AsteroidView(Transform transform, AsteroidDefinition definition)
        {
            _definition = definition;
            Transform = transform;
        }

        public Transform Transform { get; }

        public void Destroy()
        {
            if (_definition.vfxPrefab != null)
            {
                Object.Instantiate(_definition.vfxPrefab,
                    Transform.position,
                    Transform.rotation
                );
            }
            
            Object.Destroy(Transform.gameObject);
        }
    }
}