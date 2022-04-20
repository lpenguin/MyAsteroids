using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Managers.Level
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        public void ReloadLevel()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
    }
}