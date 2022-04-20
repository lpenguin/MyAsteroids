using UnityEngine.InputSystem;

namespace Game.Entities.Player
{
    public class PlayerInputController
    {
        private readonly PlayerControls _controls;
        private readonly PlayerWeaponController _weaponController;
        private readonly PlayerPhysicsController _physicsController;

        public PlayerInputController(PlayerControls controls, PlayerWeaponController weaponController,
            PlayerPhysicsController physicsController)
        {
            _controls = controls;
            _weaponController = weaponController;
            _physicsController = physicsController;
        }
        
        public void BindControls()
        {
            PlayerControls.MainActions mainActions = _controls.Main;
            
            mainActions.Shoot.performed += OnShootPrimaryPerformed;
            mainActions.Shoot.canceled += OnShootPrimaryCanceled;
            mainActions.ShootSecondary.performed += OnShootSecondaryPerformed;
            mainActions.ShootSecondary.canceled += OnShootSecondaryCanceled;
        }

        public void UnbindControls()
        {
            PlayerControls.MainActions mainActions = _controls.Main;
            
            mainActions.Shoot.performed -= OnShootPrimaryPerformed;
            mainActions.Shoot.canceled -= OnShootPrimaryCanceled;
            mainActions.ShootSecondary.performed -= OnShootSecondaryPerformed;
            mainActions.ShootSecondary.canceled -= OnShootSecondaryCanceled;
        }

        public void Update()
        {
            float acceleration = _controls.Main.Accelerate.ReadValue<float>();
            float rotation = _controls.Main.Rotate.ReadValue<float>();

            _physicsController.Rotate(rotation);
            _physicsController.Accelerate(acceleration);
        }
        
        private void OnShootSecondaryCanceled(InputAction.CallbackContext _)
        {
            _weaponController.CancelShoot(PlayerWeaponController.WeaponSlotSecondary);
        }

        private void OnShootSecondaryPerformed(InputAction.CallbackContext _)
        {
            _weaponController.Shoot(PlayerWeaponController.WeaponSlotSecondary);
        }

        private void OnShootPrimaryCanceled(InputAction.CallbackContext _)
        {
            _weaponController.CancelShoot(PlayerWeaponController.WeaponSlotPrimary);
        }

        private void OnShootPrimaryPerformed(InputAction.CallbackContext _)
        {
            _weaponController.Shoot(PlayerWeaponController.WeaponSlotPrimary);
        }
    }
}