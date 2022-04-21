using Game.Managers.GameManager;
using UnityEngine;

namespace Game.Managers.Music
{
    public class MusicManager: MonoBehaviour, IMusicManager
    {
        private IMusicSource _musicSource;
        
        private void OnEnable()
        {
            _musicSource = FindObjectOfType<MusicSource>();
        }

        private void Start()
        {
            _musicSource.Volume = GameSingleton.Instance.PreferencesManager.MusicVolume;
        }

        public float Volume
        {
            get => _musicSource.Volume;
            set => _musicSource.Volume = value;
        }
    }
}