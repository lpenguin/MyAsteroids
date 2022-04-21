using Game.Entities.HitReceiver;
using Game.Entities.Projectile;
using Game.UI;
using UnityEngine;

namespace Game.Entities.Weapon.Projectile
{
    public class ProjectileWeapon : IWeapon
    { 
        private readonly ProjectileWeaponDefinition _definition;
        private readonly Transform _owner;
        private float _cooldown;
        private bool _isShooting = false;
                
        public ProjectileWeapon(ProjectileWeaponDefinition definition, Transform owner)
        {
            _definition = definition;
            _owner = owner;
        }
    
                
        public void Shoot()
        {
            _isShooting = true;
            MakeAShot();
        }
    
        private void MakeAShot()
        {
            // TODO: Pooling
            var go = Object.Instantiate(_definition.projectilePrefab, _owner.position, _owner.rotation);
            if(go.TryGetComponent<ProjectileComponent>(out var projectileComponent))
            {
                projectileComponent.SetHitData(new HitData
                {
                    Damage = _definition.damage,
                });
            }
    
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

        public void SetupUI(IPlayerWeaponsUI gameUI)
        {
            // NOOP
        }
    }
}