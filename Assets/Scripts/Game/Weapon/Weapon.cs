using UnityEngine;

namespace Game.Weapon
{
    public abstract class Weapon: ScriptableObject
    {
        public abstract void Shoot(Transform transform);

        public abstract void CancelShoot();

        public abstract void UpdateWeapon(float timeStep);
    }
}
