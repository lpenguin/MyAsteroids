using UnityEngine;

namespace Game.Entities.Projectile
{
    public class ProjectileView
    {
        public ProjectileView(Rigidbody2D rigidbody2D, Transform transform)
        {
            Rigidbody2D = rigidbody2D;
            Transform = transform;
        }

        public Rigidbody2D Rigidbody2D { get; }
        public Transform Transform { get; }

        public void Destroy()
        {
            Object.Destroy(Transform.gameObject);
        }
    }
}