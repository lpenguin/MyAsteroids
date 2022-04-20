using Game.UI;

namespace Game.Entities.Weapon
{
    public interface IWeapon
    { 
        void Shoot();
        void CancelShoot();
        void UpdateWeapon(float timeStep);
        void SetupUI(IPlayerWeaponsUI gameUI);
    }
}