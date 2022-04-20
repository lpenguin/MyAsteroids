using UnityEngine;

namespace Game.ScreenWarp
{
    public class ScreenWarpLogic
    {
        private readonly Camera _camera;
        private readonly Transform _transform;
        private readonly Vector2 _size;
        private bool _warped = false; 

        public ScreenWarpLogic(Camera camera, Transform transform, Vector2 size)
        {
            _camera = camera;
            _transform = transform;
            _size = size;
        }

        public void UpdateTunnel()
        {
            var topLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0));
            var bottomRight = _camera.ViewportToWorldPoint(new Vector3(1, 1));
            
            var position = _transform.position;

            bool needToWarp = false;
            
            if (position.x + _size.x/2 < topLeft.x)
            {
                position.x = bottomRight.x + _size.x/2;
                needToWarp = true;
            } 
            else if (position.x - _size.x/2 > bottomRight.x)
            {
                position.x = topLeft.x - _size.x/2;
                needToWarp = true;
            }
            
            if (position.y + _size.y/2 < topLeft.y)
            {
                position.y = bottomRight.y+ _size.y/2 ;
                needToWarp = true;
            } 
            else if (position.y - _size.y/2 > bottomRight.y)
            {
                position.y = topLeft.y - _size.y/2;
                needToWarp = true;
            }

            if (!needToWarp)
            {
                _warped = false;
            }
            else if (!_warped)
            {
                _transform.position = position;
                _warped = true;
            }
        }
    }
}