using Game.Weapon;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController: GameController
    {
        private readonly Rigidbody2D _body2D;
        private readonly PlayerControls _controls;
        private readonly PlayerDefinition _parameters;
        private readonly Player _player;

        private readonly IWeapon _primaryWeapon;
        private readonly IWeapon _secondaryWeapon;
        public PlayerController(Rigidbody2D body2D, PlayerControls controls, PlayerDefinition parameters, IGameComponent gameComponent, Player player)
        {
            _body2D = body2D;
            _controls = controls;
            _parameters = parameters;
            _player = player;
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
            
            // TODO: use the playerState.score only for data pass
            _parameters.playerState.score = 0;
        }

        public override void Update(float timeStep)
        {
            _parameters.playerState.health = _player.Health;
            var accelerationInput = _controls.Main.Accelerate.ReadValue<float>();
            var rotationInput = _controls.Main.Rotate.ReadValue<float>();
            
            Vector2 force = _body2D.transform.up * accelerationInput;
            _body2D.AddForce(force, ForceMode2D.Force);
            _body2D.angularVelocity = -rotationInput * _parameters.rotationSpeed;
            
            UpdateWeapons(timeStep);
        }

        public void TakeDamage(float damage)
        {
            _player.Health = Mathf.Max(0, _player.Health - damage);
            _parameters.playerState.health = _player.Health;
            
            if (_player.Health == 0)
            {
                _parameters.playerState.TriggerPlayerDeath();
            }
        }
        
        private void UpdateWeapons(float timeStep)
        {
            _primaryWeapon.UpdateWeapon(timeStep);
            _secondaryWeapon.UpdateWeapon(timeStep);
        }
    }
}