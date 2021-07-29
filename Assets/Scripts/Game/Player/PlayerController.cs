using Game.Physics;

namespace Game.Player
{
    public class PlayerController: IGameController
    {
        
        private readonly PhysicsBody2D _body2D;
        private readonly ScreenTunnelLogic _screenTunnelLogic;
        private readonly PlayerControls _controls;
        private readonly PlayerDefinition _parameters;
        private readonly IGameView _gameView;

        public PlayerController(PhysicsBody2D body2D, ScreenTunnelLogic screenTunnelLogic, PlayerControls controls, PlayerDefinition parameters, IGameView gameView)
        {
            _body2D = body2D;
            _screenTunnelLogic = screenTunnelLogic;
            _controls = controls;
            _parameters = parameters;
            _gameView = gameView;
            var shootControl = _controls.Main.Shoot;

            var tr = _gameView.Transform;
            
            shootControl.performed += context =>
            {
                _parameters.primaryWeapon.Shoot(tr.position, tr.rotation);
            };
            
            shootControl.canceled += context =>
            {
                _parameters.primaryWeapon.CancelShoot();
            };

            var shootSecondaryControl = _controls.Main.ShootSecondary;
            shootSecondaryControl.performed += context =>
            {
                _parameters.secondaryWeapon.Shoot(tr.position, tr.rotation);
            };
            
            shootSecondaryControl.canceled += context =>
            {
                _parameters.secondaryWeapon.CancelShoot();
            };
        }

        public void Update(float timeStep)
        {
            var accelerationInput = _controls.Main.Accelerate.ReadValue<float>();
            var rotationInput = _controls.Main.Rotate.ReadValue<float>();
            _body2D.SetThrust(accelerationInput * _parameters.thrust);
            _body2D.AngularVelocity = -rotationInput * _parameters.rotationSpeed;
            _screenTunnelLogic.UpdateTunnel();
            UpdateWeapons(timeStep);
        }

        private void UpdateWeapons(float timeStep)
        {
            _parameters.primaryWeapon.UpdateWeapon(timeStep);
            _parameters.secondaryWeapon.UpdateWeapon(timeStep);
        }

        public void PhysicsUpdate(float timeStep)
        {
            _body2D.Step(timeStep);
        }
    }
}