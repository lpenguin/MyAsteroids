using Game.Player;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Weapon.Projectile
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
        public override IWeapon CreateWeapon(Transform parent, PlayerData playerData) => new ProjectileWeapon(this, parent, playerData);
    }
}