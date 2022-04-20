using UnityEngine;

namespace Game.Entities.Weapon
{
    public abstract class WeaponDefinition: ScriptableObject
    {
        public abstract IWeapon CreateWeapon(Transform owner);
    }
}
