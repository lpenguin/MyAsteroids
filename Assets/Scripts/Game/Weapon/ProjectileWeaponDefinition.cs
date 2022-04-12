﻿using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Weapon
{
    [CreateAssetMenu(menuName = "MyAsteroids/Projectile Weapon")]
    public class ProjectileWeaponDefinition: WeaponDefinition
    {

        public AssetReference projectilePrefab;
        public override IWeapon CreateWeapon(Transform parent) => new Weapon(this, parent);
        
        private class Weapon : IWeapon
        { 
            private readonly ProjectileWeaponDefinition _definition;
            private readonly Transform _parent;
            
            public Weapon(ProjectileWeaponDefinition definition, Transform parent)
            {
                _definition = definition;
                _parent = parent;
            }

            
            public void Shoot()
            {
                var handle = _definition.projectilePrefab.InstantiateAsync(_parent.position, _parent.rotation);
            }
            
            public void CancelShoot()
            {
            
            }
            
            public void UpdateWeapon(float deltaTime)
            {
            
            }
        }

        
    }
}