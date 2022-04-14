using Game.GameManager;
using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(menuName = "MyAsteroids/PlayerData Definition")]
    public class PlayerDefinition: ScriptableObject
    {
        [Header("General")]
        public PlayerState playerState;
        
        [Header("Physics")]
        public float thrust;
        public float drag;
        public float maxSpeed;
        public float rotationSpeed;
    
        [Header("Weapons")]
        public Weapon.WeaponDefinition primaryWeaponDefinition;
        public Weapon.WeaponDefinition secondaryWeaponDefinition;
    }
}