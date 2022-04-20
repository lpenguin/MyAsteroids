using System.Collections.Generic;
using Game.Entities.Weapon;

namespace Game.Entities.Player
{
    public class PlayerWeaponController
    {
        public const int WeaponSlotPrimary = 0;
        public const int WeaponSlotSecondary = 1;
        
        private readonly IWeapon[] _weapons;
        
        public PlayerWeaponController(PlayerView playerView, PlayerDefinition definition)
        {
            _weapons = new []
            {
                definition.primaryWeaponDefinition.CreateWeapon(playerView.Transform),
                definition.secondaryWeaponDefinition.CreateWeapon(playerView.Transform),
            };
        }

        public IReadOnlyCollection<IWeapon> Weapons => _weapons;
        
        public void Update(float timeDelta)
        {
            foreach (IWeapon weapon in _weapons)
            {
                weapon.UpdateWeapon(timeDelta);
            }
        }
        
        public void Shoot(int weaponSlot)
        {
            if(weaponSlot < 0 ||
               weaponSlot >= _weapons.Length)
                return;
            
            _weapons[weaponSlot].Shoot();
        }
        
        public void CancelShoot(int weaponSlot)
        {
            if(weaponSlot < 0 ||
               weaponSlot >= _weapons.Length)
                return;
            
            _weapons[weaponSlot].CancelShoot();
        }
    }
}