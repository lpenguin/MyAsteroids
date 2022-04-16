using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Asteroid
{
    [AddComponentMenu("MyAsteroids/AsteroidComponent")]
    public class AsteroidComponent: MonoBehaviour, IGameComponent, IHitReceiver
    {
        [SerializeField] 
        private AsteroidDefinition parameters;

        private AsteroidController _asteroidController;

        private void Awake()
        {
            Assert.IsTrue(TryGetComponent<Rigidbody2D>(out var body), 
                $"Must have a ${nameof(Rigidbody2D)}");

            _asteroidController = new AsteroidController(this, parameters, body);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _asteroidController.HandleCollision(col.collider);
        }
        
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