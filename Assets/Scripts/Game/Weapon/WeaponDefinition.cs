using UnityEngine;

namespace Game.Weapon
{
    public interface IWeapon
    { 
        void Shoot();
        void CancelShoot();
        void UpdateWeapon(float timeStep);
    }
    public interface IWeaponFactory
    {
        IWeapon CreateWeapon(Transform parent);
    }
    
    
    public abstract class WeaponDefinition: ScriptableObject
    {
        public delegate IWeapon WeaponFactoryDelegate(Transform parent);

        public abstract IWeapon CreateWeapon(Transform parent);
    }
}
