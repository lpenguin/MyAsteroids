using Game.Player;
using UnityEngine;

namespace Game.Weapon
{
    public interface IWeapon
    { 
        void Shoot();
        void CancelShoot();
        void UpdateWeapon(float timeStep);
    }

    public abstract class WeaponDefinition: ScriptableObject
    {
        public abstract IWeapon CreateWeapon(Transform parent, PlayerData playerData);
    }
}
