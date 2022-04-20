using Game.Entities.Player;

namespace Game.UI
{
    public interface IGameUI
    {
        // TODO: Use service locator?
        void SetPlayer(PlayerData playerData);
    }
}