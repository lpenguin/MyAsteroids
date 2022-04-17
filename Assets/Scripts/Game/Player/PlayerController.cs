using Game.Events;
using Game.HitReceiver;
using Game.Input;
using Game.Utils;
using Game.Weapon;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController: GameController
    {
        private readonly Rigidbody2D _body2D;
        private readonly PlayerDefinition _parameters;

        // TODO: weapon set?
        private readonly IWeapon _primaryWeapon;
        private readonly IWeapon _secondaryWeapon;
        public ObservableFloat Health { get; } = new ObservableFloat(1f);

        public PlayerController(Rigidbody2D body2D, PlayerDefinition parameters, IGameComponent gameComponent, PlayerData playerData)
        {
            _body2D = body2D;
            _parameters = parameters;
            var tr = gameComponent.Transform;

            _parameters.playerState.eventBus.PublishEvent(new PlayerShipAddedEvent(
                _parameters.playerState.playerData,
                this
                ));
            
            _primaryWeapon = _parameters.primaryWeaponDefinition.CreateWeapon(tr, playerData);
            _secondaryWeapon = _parameters.secondaryWeaponDefinition.CreateWeapon(tr, playerData);

            // TODO: move this to the IWeapon factory?
            _parameters.playerState.eventBus.PublishEvent(new PlayerShipWeaponAddedEvent(
                _parameters.playerState.playerData,
                this,
                _primaryWeapon
            ));
            
            _parameters.playerState.eventBus.PublishEvent(new PlayerShipWeaponAddedEvent(
                _parameters.playerState.playerData,
                this,
                _secondaryWeapon
            ));
            
            PlayerControls.MainActions mainActions = InputManager.Instance.Controls.Main;
            
            var shootControl = mainActions.Shoot;
            // TODO: unbind events
            // TODO: move controls processing to the separate controller
            shootControl.performed += _ =>
            {
                _primaryWeapon.Shoot();
            };
            
            shootControl.canceled += _ =>
            {
                _primaryWeapon.CancelShoot();
            };

            var shootSecondaryControl = mainActions.ShootSecondary;
            shootSecondaryControl.performed += _ =>
            {
                _secondaryWeapon.Shoot();
            };
            
            shootSecondaryControl.canceled += _ =>
            {
                _secondaryWeapon.CancelShoot();
            };
        }

        

        public override void Update(float timeStep)
        {
            PlayerControls.MainActions mainActions = InputManager.Instance.Controls.Main;
            var accelerationInput = mainActions.Accelerate.ReadValue<float>();
            var rotationInput = mainActions.Rotate.ReadValue<float>();
            
            Vector2 force = (Vector2)_body2D.transform.up * accelerationInput * _parameters.thrust 
                            - _body2D.velocity * _parameters.drag ;
            _body2D.velocity = Vector2.ClampMagnitude(_body2D.velocity + force * timeStep, _parameters.maxSpeed);
            _body2D.angularVelocity = -rotationInput * _parameters.rotationSpeed;
            
            UpdateWeapons(timeStep);
        }

        public void TakeDamage(float damage)
        {
            Health.Value = Mathf.Max(0, Health.Value - damage);
            
            if (Health.Value == 0)
            {
                _parameters.playerState.eventBus.PublishEvent(new PlayerShipDestroyedEvent(_parameters.playerState.playerData, this));
            }
        }
        
        private void UpdateWeapons(float timeStep)
        {
            _primaryWeapon.UpdateWeapon(timeStep);
            _secondaryWeapon.UpdateWeapon(timeStep);
        }

        public void HandleCollisionEnter(Collision2D col)
        {
            if (col.collider.TryGetComponent<IHitReceiver>(out var hitReceiver))
            {
                hitReceiver.ReceiveHit(new ReceiveHitData
                {
                    Damage = 1000.0f, // TODO: melee weapon? 
                    PlayerData = _parameters.playerState.playerData,
                    Owner = this,
                });
            }
        }
    }
}