using Game.Utils;

namespace Game.Entities.Player
{
    public class PlayerData
    {
        public ObservableInt Score { get; } = new ObservableInt(0);
        public ObservableFloat Health { get; } = new ObservableFloat(1f);
    }
}