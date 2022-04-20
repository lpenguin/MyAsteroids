using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Managers.Music
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager: MonoBehaviour, IMusicManager
    {
        public static MusicManager Instance { get; private set; }

        private AudioSource audioSource;
        
        private void Awake()
        {
            Assert.IsNull(Instance, $"{nameof(MusicManager)} singleton is already instantiated");
            Instance = this;

            audioSource = GetComponent<AudioSource>();
        }

        public float Volume
        {
            get => audioSource.volume;
            set => audioSource.volume = value;
        }
    }
}