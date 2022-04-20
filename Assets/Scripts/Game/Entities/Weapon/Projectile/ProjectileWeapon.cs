using Game.Entities.HitReceiver;
using Game.Entities.Projectile;
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
            // TODO: Asset
            var operationHandle = _definition.projectilePrefab.InstantiateAsync(_owner.position, _owner.rotation);
            operationHandle.Completed += h =>
            {
                if(h.Result.TryGetComponent<ProjectileComponent>(out var projectileComponent))
                {
                    projectileComponent.hitData = new HitData
                    {
                        Damage = _definition.damage,
                    };
                }
            };
    
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