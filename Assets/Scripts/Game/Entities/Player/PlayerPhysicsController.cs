using UnityEngine;

namespace Game.Entities.Player
{
    public class PlayerPhysicsController
    {
        private readonly PlayerView _playerView;
        private readonly PlayerDefinition _definition;

        private float _accelerationInput;
        private float _rotationInput;
        
        public PlayerPhysicsController(PlayerView playerView, PlayerDefinition definition)
        {
            _playerView = playerView;
            _definition = definition;
        }

        public void FixedUpdate(float timeStep)
        {
            var body2D = _playerView.Rigidbody2D;

            var velocity = body2D.velocity;
            Vector2 force = (Vector2)body2D.transform.up * _accelerationInput * _definition.thrust 
                            - velocity * _definition.drag;
            
            body2D.velocity = Vector2.ClampMagnitude(velocity + force * timeStep, _definition.maxSpeed);
            body2D.angularVelocity = -_rotationInput * _definition.rotationSpeed;
        }

        public void Rotate(float rotation)
        {
            _rotationInput = rotation;
        }

        public void Accelerate(float acceleration)
        {
            _accelerationInput = acceleration;
        }
    }
}