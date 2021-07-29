using Game.Physics;

namespace Game.Player
{
    public class PlayerController: GameController
    {
        
        private readonly PhysicsBody2D _body2D;
        private readonly PlayerControls _controls;
        private readonly PlayerDefinition _parameters;

        public PlayerController(PhysicsBody2D body2D, PlayerControls controls, PlayerDefinition parameters, IGameComponent gameComponent)
        {
            _body2D = body2D;
            _controls = controls;
            _parameters = parameters;
            var shootControl = _controls.Main.Shoot;

            var tr = gameComponent.Transform;
            
            shootControl.performed += context =>
            {
                _parameters.primaryWeapon.Shoot(tr);
            };
            
            shootControl.canceled += context =>
            {
                _parameters.primaryWeapon.CancelShoot();
            };

            var shootSecondaryControl = _controls.Main.ShootSecondary;
            shootSecondaryControl.performed += context =>
            {
                _parameters.secondaryWeapon.Shoot(tr);
            };
            
            shootSecondaryControl.canceled += context =>
            {
                _parameters.secondaryWeapon.CancelShoot();
            };
        }

        public override void Update(float timeStep)
        {
            var accelerationInput = _controls.Main.Accelerate.ReadValue<float>();
            var rotationInput = _controls.Main.Rotate.ReadValue<float>();
            _body2D.SetThrust(accelerationInput * _parameters.thrust);
            _body2D.AngularVelocity = -rotationInput * _parameters.rotationSpeed;
            UpdateWeapons(timeStep);
        }

        private void UpdateWeapons(float timeStep)
        {
            _parameters.primaryWeapon.UpdateWeapon(timeStep);
            _parameters.secondaryWeapon.UpdateWeapon(timeStep);
        }
    }
}