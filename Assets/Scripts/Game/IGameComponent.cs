using UnityEngine;

namespace Game
{
    // TODO: Do we need this?
    public interface IGameComponent
    {
        Transform Transform { get; }
        void DestroyGameObject();
    }
}