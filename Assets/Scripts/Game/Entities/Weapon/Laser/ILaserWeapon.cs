using Game.Utils;

namespace Game.Entities.Weapon.Laser
{
    public interface ILaserWeapon
    {
        ObservableFloat Charge { get; }
    }
}