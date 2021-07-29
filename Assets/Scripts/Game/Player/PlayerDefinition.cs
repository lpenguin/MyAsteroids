using Game.GameManager;
using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(menuName = "MyAsteroids/Player Definition")]
    public class PlayerDefinition: ScriptableObject
    {
        public float thrust;
        public float rotationSpeed;
        public GameState gameState;
    
        public Weapon.Weapon primaryWeapon;
        public Weapon.Weapon secondaryWeapon;
    }
}