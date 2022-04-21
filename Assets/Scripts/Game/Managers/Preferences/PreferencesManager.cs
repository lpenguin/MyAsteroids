using System;
using UnityEngine;

namespace Game.Managers.Preferences
{
    public class PreferencesManager : MonoBehaviour, IPreferencesManager
    {
        private const string MusicVolumePref = "audio.music.volume";
        
        public float MusicVolume
        {
            get => _musicVolume ??= PlayerPrefs.GetFloat(MusicVolumePref, 1.0f);
            set => _musicVolume = value; 
        }

        private float? _musicVolume;

        private void OnDisable()
        {
            PlayerPrefs.SetFloat(MusicVolumePref, _musicVolume ?? 1.0f);
            PlayerPrefs.Save();
        }
    }
}