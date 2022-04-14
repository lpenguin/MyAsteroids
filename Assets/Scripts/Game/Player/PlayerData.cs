using System;

namespace Game.Player
{
    public class PlayerData
    {
        public float Health { get; set; } = 1.0f;
        public int Score { get; set; }
        public float LaserCharge { get; set; } = 1.0f; // TODO: move this to LaserVeapon
    }
}