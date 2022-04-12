using System;
using Game.Utils;
using UnityEngine;

namespace Game.GameManager
{
    [CreateAssetMenu(menuName = "MyAsteroids/Game State")]
    public class GameState: ScriptableObject
    {
        public ObservableVector2 position = new();
        public ObservableFloat angle = new();
        public ObservableFloat speed = new();
        public ObservableFloat laserCharge = new();
        public ObservableFloat health = new ();
        public ObservableInt score = new();
        
        [NonSerialized]
        public Transform playerTransform;
    }
}