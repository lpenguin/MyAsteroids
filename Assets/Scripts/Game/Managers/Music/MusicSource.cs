using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Managers.Music
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicSource: MonoBehaviour, IMusicSource
    {
        private AudioSource _audioSource;
        private static MusicSource _instance;

        public float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
            Assert.IsNotNull(_audioSource, $"{nameof(_audioSource)} must be set");
        }
    }
}