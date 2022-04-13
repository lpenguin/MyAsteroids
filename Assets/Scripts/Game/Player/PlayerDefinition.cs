using Game.GameManager;
using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(menuName = "MyAsteroids/Player Definition")]
    public class PlayerDefinition: ScriptableObject
    {
        public float thrust;
        public float rotationSpeed;
        public PlayerState playerState;
    
        public Weapon.WeaponDefinition primaryWeaponDefinition;
        public Weapon.WeaponDefinition secondaryWeaponDefinition;
    }
}