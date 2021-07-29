using UnityEngine;

namespace Game.Weapon
{
    [CreateAssetMenu(menuName = "MyAsteroids/Projectile Weapon")]
    public class ProjectileWeapon: Weapon
    {
        public GameObject projectilePrefab;

        public override void Shoot(Transform transform)
        {
            var go = Instantiate(projectilePrefab, transform.position, transform.rotation);
        }

        public override void CancelShoot()
        {
        
        }

        public override void UpdateWeapon(float deltaTime)
        {
        
        }
    }
}