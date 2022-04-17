using Game.GameManager;
using UnityEngine;

namespace Game.Ufo
{
    [CreateAssetMenu(menuName = "MyAsteroids/UFO Definition")]
    public class UfoDefinition: ScriptableObject
    {
        [Header("General")]
        public PlayerState playerState;
        public int score;
        public float damage;
        
        [Header("Physics")]        
        public float speed = 2;
        public float speedSmoothness = 1;
        public float rotationSpeed = 3f;
        
        
        [Header("VFX")]
        // TODO: AssetReference
        // TODO: rename to destroyVfx
        public GameObject vfxPrefab;
    }
}