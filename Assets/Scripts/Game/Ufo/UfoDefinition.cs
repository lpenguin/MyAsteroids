using Game.GameManager;
using UnityEngine;

namespace Game.Ufo
{
    [CreateAssetMenu(menuName = "MyAsteroids/UFO Definition")]
    public class UfoDefinition: ScriptableObject
    {
        public GameState gameState;
        
        public float speed = 2;
        public float speedSmoothness = 1;
        public float lookSmoothness = 3;
    }
}