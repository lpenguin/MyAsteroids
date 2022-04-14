using Game.GameManager;
using Game.Utils;
using UnityEngine;

namespace Game.Asteroid
{
    [CreateAssetMenu(menuName = "MyAsteroids/Asteroid Definition")]
    public class AsteroidDefinition: ScriptableObject
    {
        [Header("General")]
        // TODO: move this to singleton?
        public PlayerState playerState;
        public int score;
        public float damage;
        
        [Header("Physics")]
        public RangeFloat linearVelocityRange;
        public RangeFloat angularVelocityRange;

        [Header("Spawn")]
        public GameObject spawnOnDamage;
        public int spawnCount;
        public RangeFloat spawnDistance;
    
    }
}
