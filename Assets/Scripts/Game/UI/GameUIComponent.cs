using System.Collections.Generic;
using Game.Entities.Player;
using Game.Entities.Weapon;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.UI
{
    public class GameUIComponent: MonoBehaviour
    {
        [SerializeField]
        private PlayerUIComponent playerUIComponent;

        [SerializeField]
        private GamePauseUIComponent gamePauseUIComponent;

        [SerializeField]
        private GameOverUiComponent gameOverUiComponent;

        public IPlayerFacade PlayerFacade { get; set; }

        private void Start()
        {
            Assert.IsNotNull(PlayerFacade, $"{nameof(PlayerFacade)} must be set");
            Assert.IsNotNull(PlayerFacade.PlayerData, $"{nameof(PlayerFacade.PlayerData)} must be initialized");
            Assert.IsNotNull(PlayerFacade.Weapons, $"{nameof(PlayerFacade.PlayerData)} must be initialized");
            
            SetupPlayerUI(PlayerFacade.PlayerData);
            SetupWeaponsUI(PlayerFacade.Weapons);
        }

        private void SetupWeaponsUI(IReadOnlyCollection<IWeapon> weapons)
        {
            foreach (IWeapon weapon in weapons)
            {
                weapon.SetupUI(playerUIComponent);
            }
        }

        private void SetupPlayerUI(PlayerData playerData)
        {
            playerUIComponent.SetPlayer(playerData);
            gameOverUiComponent.SetPlayer(playerData);
            gamePauseUIComponent.SetPlayer(playerData);
        }
    }
}