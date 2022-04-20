using Game.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
        public AssetReference spawnOnDamage;
        public int spawnCount;
        public RangeFloat spawnDistance;

        [Header("VFX")]
        // TODO: pooling
        public AssetReference vfxPrefab;
    }
}
