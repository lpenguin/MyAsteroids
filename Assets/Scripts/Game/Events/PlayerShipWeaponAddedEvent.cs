using Game.Player;
using Game.Weapon;

namespace Game.Events
{
    public record PlayerShipWeaponAddedEvent(PlayerData PlayerData, PlayerController PlayerController, IWeapon Weapon);
}