using UnityEngine;

namespace Game.Weapon
{
    [CreateAssetMenu(menuName = "MyAsteroids/Projectile Weapon")]
    public class ProjectileWeapon: Weapon
    {
        public GameObject projectilePrefab;

        public override void Shoot(Vector3 position, Quaternion rotation)
        {
            var go = Object.Instantiate(projectilePrefab, position, rotation);
        }

        public override void CancelShoot()
        {
        
        }

        public override void UpdateWeapon(float deltaTime)
        {
        
        }
    }
}