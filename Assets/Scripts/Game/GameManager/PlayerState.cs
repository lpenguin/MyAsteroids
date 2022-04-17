using System;
using Game.Events;
using Game.Player;
using UnityEngine;

namespace Game.GameManager
{
    // TODO: rename to GameState
    // TODO: use singleton?
    [CreateAssetMenu(menuName = "MyAsteroids/Player State")]
    public class PlayerState: ScriptableObject
    {
        [NonSerialized]
        public PlayerData playerData = new PlayerData();
        
        [NonSerialized]
        public Transform playerTransform;

        [NonSerialized]
        public AudioListener audioListener;

        // TODO: is it per player event bus or global one?
        [NonSerialized]
        public EventBus eventBus;
    }
}