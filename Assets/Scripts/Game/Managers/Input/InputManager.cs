using UnityEngine;

namespace Game.Managers.Input
{
    public class InputManager: MonoBehaviour, IInputManger
    {
        public PlayerControls PlayerControls { get; private set; }

        private void Awake()
        {
            PlayerControls ??= new PlayerControls();
        }

        private void OnEnable()
        {
            PlayerControls.Enable();
        }

        private void OnDisable()
        {
            PlayerControls.Disable();
        }
    }
}