using System;
using Game.Physics;
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
            Assert.IsTrue(TryGetComponent<PhysicsBody2DComponent>(out var physicsBody2DComponent), "Must have a PhysicsBody2DComponent");

            var body2D = physicsBody2DComponent.Body2D;
            _projectileController = new ProjectileController(body2D, projectileParameters, this);
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