using Game.Entities.Player;
using Game.Events;
using Game.Managers.Input;
using Game.Managers.Level;
using Game.Managers.Music;
using Game.Managers.Pause;
using Game.Managers.Preferences;
using Game.UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Managers.GameManager
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private PlayerComponent playerComponent;

        [SerializeField]
        private InputManager inputManager;

        [SerializeField]
        private GameUIComponent gameUIComponent;

        [SerializeField]
        private PauseManager pauseManager;

        [SerializeField]
        private LevelManager levelManager;

        [SerializeField]
        private PreferencesManager preferencesManager;
        
        [SerializeField]
        private MusicManager musicManager;
        
        void Awake()
        {
            Assert.IsNotNull(playerComponent, $"{nameof(playerComponent)} must be set");
            Assert.IsNotNull(inputManager, $"{nameof(inputManager)} must be set");
            Assert.IsNotNull(pauseManager, $"{nameof(pauseManager)} must be set");
            Assert.IsNotNull(levelManager, $"{nameof(levelManager)} must be set");
            Assert.IsNotNull(musicManager, $"{nameof(musicManager)} must be set");
            Assert.IsNotNull(gameUIComponent, $"{nameof(gameUIComponent)} must be set");
            Assert.IsNotNull(preferencesManager, $"{nameof(preferencesManager)} must be set");
            
            GameSingleton.Instance.EventBus = new EventBus();
            GameSingleton.Instance.PlayerTransform = playerComponent.transform;
            GameSingleton.Instance.InputManger = inputManager;
            GameSingleton.Instance.PauseManager = pauseManager;
            GameSingleton.Instance.LevelManager = levelManager;
            GameSingleton.Instance.MusicManager = musicManager;
            GameSingleton.Instance.PreferencesManager = preferencesManager;
            
            gameUIComponent.PlayerFacade = playerComponent;
        }
    }
}