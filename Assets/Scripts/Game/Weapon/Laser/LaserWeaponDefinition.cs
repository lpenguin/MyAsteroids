using Game.GameManager;
using Game.Player;
using UnityEngine;

namespace Game.Weapon.Laser
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

        [SerializeField]
        public PlayerState playerState;

        public override IWeapon CreateWeapon(Transform parent, PlayerData playerData) => new LaserWeapon(this, parent, playerData);
    }
}