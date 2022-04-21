using Game.Utils;
using UnityEngine;

namespace Game.Entities.Asteroid
{
    [CreateAssetMenu(menuName = "MyAsteroids/Asteroid Definition")]
    public class AsteroidDefinition: ScriptableObject
    {
        [Header("General")]
        public int score;
        public float damage;
        
        [Header("Physics")]
        public RangeFloat linearVelocityRange;
        public RangeFloat angularVelocityRange;

        [Header("Spawn")]
        // TODO: pooling
        public GameObject spawnOnDamage;
        public int spawnCount;
        public RangeFloat spawnDistance;

        [Header("VFX")]
        // TODO: pooling
        public GameObject vfxPrefab;
    }
}
