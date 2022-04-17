using Game.Player;

namespace Game.HitReceiver
{
    public class ReceiveHitData
    {
        public float Damage { get; set; }
        public PlayerData PlayerData { get; set; }
        
        // TODO: object is too broad
        // TODO: used only for to determine is it LaserWeapon or not 
        public object Owner { get; set; }
    }
}