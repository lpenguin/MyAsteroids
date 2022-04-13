using Game.Physics;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Asteroid
{
    [AddComponentMenu("MyAsteroids/AsteroidComponent")]
    public class AsteroidComponent: MonoBehaviour, OffScreenSpawner.IHasSpawnerParent, IGameComponent, IHitReceiver
    {
        [SerializeField] 
        private AsteroidDefinition parameters;

        private AsteroidController _asteroidController;

        private void Start()
        {
            Assert.IsTrue(TryGetComponent<PhysicsBody2DComponent>(out var physicsBody2DComponent), 
                "Must have a PhysicsBody2DComponent");

            var body = physicsBody2DComponent.Body2D;
            _asteroidController = new AsteroidController(this, parameters, body);
        }

        private void OnDestroy()
        {
            // TODO: move to controller
            Spawner?.Decrement();
        }

        // TODO: move to controller
        public OffScreenSpawner Spawner { get; set; }
        public Transform Transform => transform;
        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }

        public void ReceiveHit(float damage)
        {
            _asteroidController.ReceiveHit();
        }
    }
}