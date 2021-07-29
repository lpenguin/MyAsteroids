using System;
using UnityEngine;

namespace Game.ScreenWarp
{
    [AddComponentMenu("MyAsteroids/ScreenWarpComponent")]
    public class ScreenWarpComponent: MonoBehaviour
    {
        private ScreenWarpLogic _screenWarpLogic;

        private void Awake()
        {
            Vector2 size = Vector2.zero;
            if (TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                size = spriteRenderer.bounds.size;
            }

            _screenWarpLogic = new ScreenWarpLogic(Camera.main, transform, size);
        }

        private void LateUpdate()
        {
            _screenWarpLogic.UpdateTunnel();
        }
    }
}