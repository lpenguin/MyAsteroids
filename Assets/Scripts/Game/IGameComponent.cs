using UnityEngine;

namespace Game
{
    // TODO: Do we need this?
    // TODO: Make an abstract class
    public interface IGameComponent
    {
        Transform Transform { get; }
        void DestroyGameObject();
    }
}