using UnityEngine;

namespace Game.Weapon
{
    public abstract class Weapon: ScriptableObject
    {
        public abstract void Shoot(Vector3 position, Quaternion quaternion);

        public abstract void CancelShoot();

        public abstract void UpdateWeapon(float timeStep);
    }
}
