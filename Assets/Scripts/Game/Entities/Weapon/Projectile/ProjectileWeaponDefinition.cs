using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Entities.Weapon.Projectile
{
    [CreateAssetMenu(menuName = "MyAsteroids/Projectile Weapon")]
    public class ProjectileWeaponDefinition: WeaponDefinition
    {
        [SerializeField]
        public float damage = 1f;
        
        [SerializeField]
        public float period = 0.2f;
        
        [SerializeField]
        public AssetReference projectilePrefab;
        public override IWeapon CreateWeapon(Transform owner) => new ProjectileWeapon(this, owner);
    }
}