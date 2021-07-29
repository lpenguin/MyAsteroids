using UnityEngine;

namespace Game
{
    // Do we need this?
    public interface IGameView
    {
        Transform Transform { get; }
        void DestroyGameObject();
    }
}