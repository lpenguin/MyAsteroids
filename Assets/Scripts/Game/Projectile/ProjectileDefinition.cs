using UnityEngine;

namespace Game.Projectile
{
    [CreateAssetMenu(menuName = "MyAsteroids/Projectile Definition")]
    public class ProjectileDefinition: ScriptableObject
    {
        public float speed = 3;
    }
}