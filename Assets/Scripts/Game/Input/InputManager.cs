using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Input
{
    public class InputManager: MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public PlayerControls Controls { get; private set; }

        private void Awake()
        {
            Assert.IsNull(Instance, $"{nameof(InputManager)} singleton is already instantiated");
            Instance = this;
            Controls ??= new PlayerControls();
        }

        private void OnEnable()
        {
            Controls.Enable();
        }

        private void OnDisable()
        {
            Controls.Disable();
        }
    }
}