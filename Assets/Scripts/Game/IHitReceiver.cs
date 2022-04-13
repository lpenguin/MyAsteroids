namespace Game
{
    // TODO: make an additional interface "IHasHitReceiver"?
    public interface IHitReceiver
    {
        void ReceiveHit(float damage);
    }
}