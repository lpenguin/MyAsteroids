using UnityEngine;

namespace Game.Managers.Pause
{
    public class PauseManager : MonoBehaviour, IPauseManager
    {
        public void SetPaused(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}