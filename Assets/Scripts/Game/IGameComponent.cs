using UnityEngine;

namespace Game
{
    // Do we need this?
    public interface IGameComponent
    {
        Transform Transform { get; }
        void DestroyGameObject();
    }
}