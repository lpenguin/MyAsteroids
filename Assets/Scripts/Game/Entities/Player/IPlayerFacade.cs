using System.Collections.Generic;
using Game.Entities.Weapon;

namespace Game.Entities.Player
{
    public interface IPlayerFacade
    {
        IReadOnlyCollection<IWeapon> Weapons { get; }
        PlayerData PlayerData { get; }
    }
}