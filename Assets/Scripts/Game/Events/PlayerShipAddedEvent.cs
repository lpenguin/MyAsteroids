using Game.Player;

namespace Game.Events
{
    public record PlayerShipAddedEvent(PlayerData PlayerData, PlayerController PlayerController);
}