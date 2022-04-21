using Game.Managers.GameManager;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Managers.Music
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager: MonoBehaviour, IMusicManager
    {
        private AudioSource audioSource;
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            Assert.IsNotNull(audioSource, $"{nameof(audioSource)} must be set");
        }

        private void Start()
        {
            audioSource.volume = GameSingleton.Instance.PreferencesManager.MusicVolume;
        }

        public float Volume
        {
            get => audioSource.volume;
            set => audioSource.volume = value;
        }
    }
}