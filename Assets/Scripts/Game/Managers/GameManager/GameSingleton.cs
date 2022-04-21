using Game.Events;
using Game.Managers.Input;
using Game.Managers.Level;
using Game.Managers.Music;
using Game.Managers.Pause;
using Game.Managers.Preferences;
using Game.UI;
using Game.Utils;
using UnityEngine;

namespace Game.Managers.GameManager
{
    public class GameSingleton : Singleton<GameSingleton>
    {
        public Transform PlayerTransform { get; set; }
        public EventBus EventBus { get; set; }
        
        public IInputManger InputManger { get; set; }
        public IPauseManager PauseManager { get; set; }
        public ILevelManager LevelManager { get; set; }
        public IMusicManager MusicManager { get; set; }
        public IPreferencesManager PreferencesManager { get; set; }
    }
}