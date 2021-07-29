using Game.Physics;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Player
{
    // TODO: RequireComponent generates meaningless "creating missing component messages in the prefab mode"
    [AddComponentMenu("MyAsteroids/PlayerComponent")]
    public class PlayerComponent: MonoBehaviour, IGameComponent
    {
        [SerializeField] 
        private PlayerDefinition playerParameters;

        private PlayerController _playerController;

        private PlayerControls _controls;
        private void Start()
        {
            Assert.IsTrue(TryGetComponent<PhysicsBody2DComponent>(out var physicsBody2DComponent), 
                "Must have a PhysicsBody2DComponent");

            var body = physicsBody2DComponent.Body2D;
            _playerController = new PlayerController(body, _controls, playerParameters, this);
        }

        private void OnEnable()
        {
            _controls ??= new PlayerControls();
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Update()
        {
            _playerController.Update(Time.deltaTime);
        }


        public Transform Transform => transform;
        
        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
}