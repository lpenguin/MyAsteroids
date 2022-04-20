using Game.Entities.Player;
using UnityEngine;

namespace Game.UI
{
    public class GameUIComponent: MonoBehaviour, IGameUI
    {
        [SerializeField]
        private PlayerUIComponent playerUIComponent;

        [SerializeField]
        private GamePauseUIComponent gamePauseUIComponent;

        [SerializeField]
        private GameOverUiComponent gameOverUiComponent;

        public void SetPlayer(PlayerData playerData)
        {
            playerUIComponent.SetPlayer(playerData);
            gameOverUiComponent.SetPlayer(playerData);
            gamePauseUIComponent.SetPlayer(playerData);
        }
    }
}