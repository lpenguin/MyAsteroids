namespace Game.Weapon
{
    public interface IWeapon
    { 
        void Shoot();
        void CancelShoot();
        void UpdateWeapon(float timeStep);
    }
}