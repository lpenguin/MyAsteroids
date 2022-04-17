using Game.Player;

namespace Game.Events
{
    public record PlayerShipDestroyedEvent(PlayerData PlayerData, PlayerController PlayerController);
}