using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Projectile
{
    [AddComponentMenu("MyAsteroids/ProjectileComponent")]
    public class ProjectileComponent: MonoBehaviour, IGameComponent
    {
        [SerializeField] 
        private ProjectileDefinition projectileParameters;
        
        private ProjectileController _projectileController;
        private void Start()
        {
            Assert.IsTrue(TryGetComponent<Rigidbody2D>(out var body2D), 
                $"Must have a {nameof(Rigidbody2D)}");

            _projectileController = new ProjectileController(projectileParameters, this, body2D);
        }

        private void OnEnable()
        {
            _projectileController?.OnEnable();
        }

        private void OnDisable()
        {
            _projectileController?.OnDisable();
        }

        private void FixedUpdate()
        {
            _projectileController.PhysicsUpdate(Time.fixedDeltaTime);
        }

        private void Update()
        {
            _projectileController.Update(Time.deltaTime);
        }

        public Transform Transform => transform;
        
        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
}