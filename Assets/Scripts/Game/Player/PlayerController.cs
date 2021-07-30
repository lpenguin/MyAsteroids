using Game.Physics;
using Game.Weapon;

namespace Game.Player
{
    public class PlayerController: GameController
    {
        
        private readonly PhysicsBody2D _body2D;
        private readonly PlayerControls _controls;
        private readonly PlayerDefinition _parameters;

        private readonly IWeapon _primaryWeapon;
        private readonly IWeapon _secondaryWeapon;
        public PlayerController(PhysicsBody2D body2D, PlayerControls controls, PlayerDefinition parameters, IGameComponent gameComponent)
        {
            _body2D = body2D;
            _controls = controls;
            _parameters = parameters;
            var tr = gameComponent.Transform;

            _primaryWeapon = _parameters.primaryWeaponDefinition.CreateWeapon(tr);
            _secondaryWeapon = _parameters.secondaryWeaponDefinition.CreateWeapon(tr);

            var shootControl = _controls.Main.Shoot;
            shootControl.performed += context =>
            {
                _primaryWeapon.Shoot();
            };
            
            shootControl.canceled += context =>
            {
                _primaryWeapon.CancelShoot();
            };

            var shootSecondaryControl = _controls.Main.ShootSecondary;
            shootSecondaryControl.performed += context =>
            {
                _secondaryWeapon.Shoot();
            };
            
            shootSecondaryControl.canceled += context =>
            {
                _secondaryWeapon.CancelShoot();
            };
        }

        public override void Update(float timeStep)
        {
            var accelerationInput = _controls.Main.Accelerate.ReadValue<float>();
            var rotationInput = _controls.Main.Rotate.ReadValue<float>();
            _body2D.SetThrust(accelerationInput * _parameters.thrust);
            _body2D.AngularVelocity = -rotationInput * _parameters.rotationSpeed;
            UpdateWeapons(timeStep);
            
            _parameters.gameState.speed.Set(_body2D.Velocity.magnitude);
            _parameters.gameState.position.Set(_body2D.Position);
            _parameters.gameState.angle.Set(_body2D.Rotation);
            
        }

        private void UpdateWeapons(float timeStep)
        {
            _primaryWeapon.UpdateWeapon(timeStep);
            _secondaryWeapon.UpdateWeapon(timeStep);
        }
    }
}