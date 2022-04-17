using Game.Player;
using UnityEngine;

namespace Game.Weapon
{
    public abstract class WeaponDefinition: ScriptableObject
    {
        public abstract IWeapon CreateWeapon(Transform parent, PlayerData playerData);
    }
}
