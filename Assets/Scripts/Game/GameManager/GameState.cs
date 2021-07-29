using System;
using Game.Utils;
using UnityEngine;

namespace Game.GameManager
{
    [CreateAssetMenu(menuName = "MyAsteroids/Game State")]
    public class GameState: ScriptableObject
    {
        public ObservableValue<Vector2> position = new ObservableValue<Vector2>();
        public ObservableValue<float> angle = new ObservableValue<float>();
        public ObservableValue<float> speed = new ObservableValue<float>();
        public ObservableValue<float> laserCapacity = new ObservableValue<float>();
        public ObservableValue<int> score = new ObservableValue<int>();

        [NonSerialized]
        public Transform playerTransform;
    }
}