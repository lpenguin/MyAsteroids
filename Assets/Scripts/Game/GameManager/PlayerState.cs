using System;
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

        // TODO: Use event bus?
        public event Action OnPlayerDeath;

        public void TriggerPlayerDeath()
        {
            OnPlayerDeath?.Invoke();
        }
    }
}