namespace Game.Entities.HitReceiver
{
    public enum DamageType
    {
        Normal,
        PreventSpawning,
    }
    public class HitData
    {
        public float Damage { get; set; }
        public DamageType DamageType { get; set; } = DamageType.Normal;
    }
}