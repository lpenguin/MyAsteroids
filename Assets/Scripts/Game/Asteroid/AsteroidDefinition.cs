using Game.Utils;
using UnityEngine;

namespace Game.Asteroid
{
    [CreateAssetMenu(menuName = "MyAsteroids/Asteroid Definition")]
    public class AsteroidDefinition: ScriptableObject
    {
        [Header("Velocity")]
        public RangeFloat linearVelocityRange;
    
        public RangeFloat angularVelocityRange;

        [Header("Spawn")]
        public GameObject spawnOnDamage;
        public int spawnCount;
        public RangeFloat spawnDistance;
    
    }
}
