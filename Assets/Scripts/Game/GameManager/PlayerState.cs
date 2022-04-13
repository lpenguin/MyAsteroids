using System;
using UnityEngine;

namespace Game.GameManager
{
    [CreateAssetMenu(menuName = "MyAsteroids/Player State")]
    public class PlayerState: ScriptableObject
    {
        public float laserCharge;
        public float health;
        public int score;
        
        [NonSerialized]
        public Transform playerTransform;

        public event Action OnPlayerDeath;

        public void TriggerPlayerDeath()
        {
            OnPlayerDeath?.Invoke();
        }
    }
}