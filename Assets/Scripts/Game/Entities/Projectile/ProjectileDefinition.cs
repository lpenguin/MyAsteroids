using UnityEngine;

namespace Game.Entities.Projectile
{
    [CreateAssetMenu(menuName = "MyAsteroids/Projectile Definition")]
    public class ProjectileDefinition: ScriptableObject
    {
        public float speed = 3;
    }
}