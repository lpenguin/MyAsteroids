using Game.Weapon;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController: GameController
    {
        private readonly Rigidbody2D _body2D;
        private readonly PlayerControls _controls;
        private readonly PlayerDefinition _parameters;

        private readonly IWeapon _primaryWeapon;
        private readonly IWeapon _secondaryWeapon;
        public PlayerController(Rigidbody2D body2D, PlayerControls controls, PlayerDefinition parameters, IGameComponent gameComponent, PlayerData playerData)
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
            
            // TODO: must not keep old playerData 
            _parameters.playerState.playerData = new PlayerData();
        }

        public override void Update(float timeStep)
        {
            var accelerationInput = _controls.Main.Accelerate.ReadValue<float>();
            var rotationInput = _controls.Main.Rotate.ReadValue<float>();
            
            Vector2 force = (Vector2)_body2D.transform.up * accelerationInput * _parameters.thrust 
                            - _body2D.velocity * _parameters.drag ;
            _body2D.velocity = Vector2.ClampMagnitude(_body2D.velocity + force * timeStep, _parameters.maxSpeed);
            _body2D.angularVelocity = -rotationInput * _parameters.rotationSpeed;
            
            UpdateWeapons(timeStep);
        }

        public void TakeDamage(float damage)
        {
            _parameters.playerState.playerData.Health = Mathf.Max(0, _parameters.playerState.playerData.Health - damage);
            
            if (_parameters.playerState.playerData.Health == 0)
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