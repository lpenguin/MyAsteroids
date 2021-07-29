using UnityEngine;

namespace Game
{
    public class ScreenTunnelLogic
    {
        private readonly Camera _camera;
        private readonly Transform _transform;
        private readonly Vector2 _size;

        public ScreenTunnelLogic(Camera camera, Transform transform, Vector2 size)
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
            
            if (position.x + _size.x/2 < topLeft.x)
            {
                position.x = bottomRight.x + _size.x/2;
            } 
            else if (position.x - _size.x/2 > bottomRight.x)
            {
                position.x = topLeft.x - _size.x/2;
            }
            
            if (position.y + _size.y/2 < topLeft.y)
            {
                position.y = bottomRight.y+ _size.y/2 ;
            } 
            else if (position.y - _size.y/2 > bottomRight.y)
            {
                position.y = topLeft.y - _size.y/2;
            }

            _transform.position = position;
        }
    }
}