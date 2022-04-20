using System;
using Game.Entities.HitReceiver;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Entities.Projectile
{
    [AddComponentMenu("MyAsteroids/ProjectileComponent")]
    public class ProjectileComponent: MonoBehaviour
    {
        [SerializeField] 
        private ProjectileDefinition definition;

        [NonSerialized]
        public HitData hitData; 
            
        private ProjectileController _projectileController;
        private void Start()
        {
            Assert.IsNotNull(definition, $"{nameof(definition)} must be set");
            
            var rigidbody2D = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(rigidbody2D, $"Must have a {nameof(Rigidbody2D)}");

            ProjectileView view = new ProjectileView(rigidbody2D, transform);

            _projectileController = new ProjectileController(view, definition, hitData);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _projectileController.HandleCollisionEnter(col);
        }
    }
}