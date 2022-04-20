using UnityEngine;

namespace Game.Managers.Music
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager: MonoBehaviour, IMusicManager
    {
        private AudioSource audioSource;
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public float Volume
        {
            get => audioSource.volume;
            set => audioSource.volume = value;
        }
    }
}