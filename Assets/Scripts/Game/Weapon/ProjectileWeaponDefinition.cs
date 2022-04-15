using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Weapon
{
    [CreateAssetMenu(menuName = "MyAsteroids/Projectile Weapon")]
    public class ProjectileWeaponDefinition: WeaponDefinition
    {
        [SerializeField]
        private float period = 0.2f;
        
        [SerializeField]
        private AssetReference projectilePrefab;
        public override IWeapon CreateWeapon(Transform parent) => new Weapon(this, parent);
        
        private class Weapon : IWeapon
        { 
            private readonly ProjectileWeaponDefinition _definition;
            private readonly Transform _parent;
            private float _cooldown;
            private bool _isShooting = false;
            
            public Weapon(ProjectileWeaponDefinition definition, Transform parent)
            {
                _definition = definition;
                _parent = parent;
            }

            
            public void Shoot()
            {
                _isShooting = true;
                MakeAShot();
            }

            private void MakeAShot()
            {
                _definition.projectilePrefab.InstantiateAsync(_parent.position, _parent.rotation);
                _cooldown = _definition.period;
            }

            public void CancelShoot()
            {
                _isShooting = false;
            }
            
            public void UpdateWeapon(float deltaTime)
            {
                if (!_isShooting)
                {
                    return;
                }
                
                _cooldown -= deltaTime;
                if (_cooldown <= 0)
                {
                    MakeAShot();
                }
            }
        }

        
    }
}