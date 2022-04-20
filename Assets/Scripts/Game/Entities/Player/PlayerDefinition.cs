using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Entities.Player
{
    [CreateAssetMenu(menuName = "MyAsteroids/PlayerData Definition")]
    public class PlayerDefinition: ScriptableObject
    {
        [Header("Physics")]
        public float thrust;
        public float drag;
        public float maxSpeed;
        public float rotationSpeed;

        [Header("Collision")]
        public float collisionDamage = 100.0f;
        
        [Header("Weapons")]
        public Weapon.WeaponDefinition primaryWeaponDefinition;
        public Weapon.WeaponDefinition secondaryWeaponDefinition;

        [Header("VFX")]
        public AssetReference destroyVfx;
    }
}