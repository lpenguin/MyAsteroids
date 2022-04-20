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

        private readonly ProjectileModel _model = new();
            
        private ProjectileController _projectileController;
        
        private void Start()
        {
            Assert.IsNotNull(definition, $"{nameof(definition)} must be set");
            
            var body2D = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(body2D, $"Must have a {nameof(Rigidbody2D)}");

            ProjectileView view = new ProjectileView(body2D, transform);
            _projectileController = new ProjectileController(view, definition, _model);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _projectileController.HandleCollisionEnter(col);
        }

        public void SetHitData(HitData hitData)
        {
            _model.HitData = hitData;
        }
    }
}