using Game.Physics;
using Game.Utils;
using UnityEngine;

namespace Game.Asteroid
{
    public class AsteroidComponent: MonoBehaviour, OffScreenSpawner.IHasSpawnerParent, IGameView, IHitReceiver
    {
        [SerializeField] 
        private AsteroidDefinition parameters;

        [SerializeField]
        private PhysicsBody2DDefinition bodyParameters;
        
        private AsteroidController _asteroidController;

        private void Start()
        {
            Vector2 size = Vector2.zero;
            if (TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                size = spriteRenderer.bounds.size;
            }

            IPhysicsBody2DShape shape = null;
            if (TryGetComponent<CircleCollider2D>(out var circleCollider))
            {
                shape = new CirclePhysicsBody2DShape(circleCollider);
            }
            
            
            var body = new PhysicsBody2D(transform, bodyParameters, shape)
            {
                Velocity = parameters.linearVelocityRange.RandomVector2(),
                AngularVelocity = parameters.angularVelocityRange.RandomFloat(),
            };
            
            var tunnelController = new ScreenTunnelLogic(Camera.main, transform, size);
            _asteroidController = new AsteroidController(body, tunnelController, parameters, this);
        }

        private void Update()
        {
            _asteroidController.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _asteroidController.PhysicsUpdate(Time.fixedDeltaTime);
        }

        private void OnDestroy()
        {
            // TODO: move to controller
            Spawner?.Decrement();
        }

        public OffScreenSpawner Spawner { get; set; }
        public Transform Transform => transform;
        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }

        public void ReceiveHit()
        {
            _asteroidController.ReceiveHit();
        }
    }
}