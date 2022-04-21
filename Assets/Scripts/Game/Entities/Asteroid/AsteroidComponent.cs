using Game.Entities.HitReceiver;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Entities.Asteroid
{
    [AddComponentMenu("MyAsteroids/AsteroidComponent")]
    public class AsteroidComponent: MonoBehaviour, IHitReceiver
    {
        [SerializeField] 
        private AsteroidDefinition parameters;

        private AsteroidController _asteroidController;

        private void Awake()
        {
            Assert.IsNotNull(parameters, $"{nameof(parameters)} must be set");
            var body = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(body, $"Must have a ${nameof(Rigidbody2D)}");

            AsteroidView asteroidView = new AsteroidView(transform, parameters);
            _asteroidController = new AsteroidController(asteroidView, parameters, body);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _asteroidController.HandleCollision(col.collider);
        }
        
        public void ReceiveHit(HitData data)
        {
            _asteroidController.ReceiveHit(data);
        }
    }
}