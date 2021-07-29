namespace Game
{
    public interface IGameController
    {
        void Update(float timeStep);
        void PhysicsUpdate(float timeStep);
    }
}