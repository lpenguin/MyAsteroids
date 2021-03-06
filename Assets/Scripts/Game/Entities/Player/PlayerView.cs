using UnityEngine;

namespace Game.Entities.Player
{
    public class PlayerView
    {
        public Transform Transform { get; }
        public Rigidbody2D Rigidbody2D { get; }
        
        private PlayerDefinition PlayerDefinition { get; }
        private SpriteRenderer SpriteRenderer { get; }
        
        public PlayerView(
            Transform transform, 
            Rigidbody2D rigidbody2D,
            PlayerDefinition playerDefinition,
            SpriteRenderer spriteRenderer
            )
        {
            PlayerDefinition = playerDefinition;
            SpriteRenderer = spriteRenderer;
            Transform = transform;
            Rigidbody2D = rigidbody2D;
        }

        public void ShowPlayerDeath()
        {
            SpriteRenderer.enabled = false;
        }
    }
}