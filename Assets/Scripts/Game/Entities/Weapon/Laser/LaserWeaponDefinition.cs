using UnityEngine;

namespace Game.Entities.Weapon.Laser
{
    [CreateAssetMenu(menuName = "MyAsteroids/Laser Weapon Definition")]
    public class LaserWeaponDefinition: WeaponDefinition
    {

        [Header("General")]
        [SerializeField]
        public float damage = 1f;
        
        [SerializeField]
        public float firstShotAmmo = 0.33f;
        
        [SerializeField]
        public float ammoPerSec = 0.3f;

        [SerializeField]
        public float restorePerSec = 0.1f;

        [Header("Collision")]
        [SerializeField]
        public float traceStep = 5;

        [SerializeField]
        public LayerMask hitMask;

        [SerializeField]
        public GameObject laserEffectPrefab;
        

        public override IWeapon CreateWeapon(Transform owner) => new LaserWeapon(this, owner);
    }
}