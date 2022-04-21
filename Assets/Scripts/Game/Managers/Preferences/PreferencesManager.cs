using UnityEngine;

namespace Game.Managers.Preferences
{
    public class PreferencesManager : MonoBehaviour, IPreferencesManager
    {
        private const string MusicVolumePref = "audio.music.volume";
        
        public float MusicVolume { get; set; }

        private void OnEnable()
        {
            MusicVolume = PlayerPrefs.GetFloat(MusicVolumePref);
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat(MusicVolumePref, MusicVolume);
            PlayerPrefs.Save();
        }
    }
}