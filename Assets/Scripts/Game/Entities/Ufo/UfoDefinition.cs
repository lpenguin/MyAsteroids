using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Entities.Ufo
{
    [CreateAssetMenu(menuName = "MyAsteroids/UFO Definition")]
    public class UfoDefinition: ScriptableObject
    {
        [Header("General")]
        public int score;
        public float damage;
        
        [Header("Physics")]        
        public float speed = 2;
        public float speedSmoothness = 1;
        public float rotationSpeed = 3f;


        [Header("VFX")]
        public AssetReference destroyVfx;
    }
}