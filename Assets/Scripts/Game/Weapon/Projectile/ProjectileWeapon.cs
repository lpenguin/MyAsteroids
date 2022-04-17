using Game.HitReceiver;
using Game.Player;
using Game.Projectile;
using UnityEngine;

namespace Game.Weapon.Projectile
{
    public class ProjectileWeapon : IWeapon
    { 
        private readonly ProjectileWeaponDefinition _definition;
        private readonly Transform _parent;
        private readonly PlayerData _playerData;
        private float _cooldown;
        private bool _isShooting = false;
                
        public ProjectileWeapon(ProjectileWeaponDefinition definition, Transform parent, PlayerData playerData)
        {
            _definition = definition;
            _parent = parent;
            _playerData = playerData;
        }
    
                
        public void Shoot()
        {
            _isShooting = true;
            MakeAShot();
        }
    
        private void MakeAShot()
        {
            var operationHandle = _definition.projectilePrefab.InstantiateAsync(_parent.position, _parent.rotation);
            operationHandle.Completed += h =>
            {
                if(h.Result.TryGetComponent<ProjectileComponent>(out var projectileComponent))
                {
                    projectileComponent.receiveHitData = new ReceiveHitData
                    {
                        Damage = _definition.damage,
                        PlayerData = _playerData,
                        Owner = this,
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