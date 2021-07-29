using Game.Physics;
using UnityEngine;

namespace Game.Projectile
{
    public class ProjectileComponent: MonoBehaviour, IGameView
    {
        [SerializeField] 
        private ProjectileDefinition projectileParameters;
        
        [SerializeField] 
        private PhysicsBody2DDefinition bodyParameters;

        private ProjectileController _projectileController;
        private void Awake()
        {
            IPhysicsBody2DShape shape = null;
            if (TryGetComponent<CircleCollider2D>(out var circleCollider))
            {
                shape = new CirclePhysicsBody2DShape(circleCollider);
            }
            
            var body2D = new PhysicsBody2D(transform, bodyParameters, shape);
            _projectileController = new ProjectileController(body2D, projectileParameters, this);
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