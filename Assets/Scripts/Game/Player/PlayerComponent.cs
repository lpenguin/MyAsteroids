using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Player
{
    [AddComponentMenu("MyAsteroids/PlayerComponent")]
    public class PlayerComponent: MonoBehaviour, IGameComponent, IHitReceiver
    {
        [SerializeField] 
        private PlayerDefinition playerParameters;

        [SerializeField]
        private PlayerData _playerData;
        
        private PlayerController _playerController;

        private PlayerControls _controls;

        private void Awake()
        {
            // TODO: move to controller
            playerParameters.playerState.playerTransform = transform;
        }

        private void Start()
        {
            Assert.IsTrue(TryGetComponent<Rigidbody2D>(out var physicsBody2DComponent), 
                "Must have a PhysicsBody2DComponent");

            var body = physicsBody2DComponent;
            _playerData = new PlayerData
            {
                Health = 1f,
            };
            _playerController = new PlayerController(body, _controls, playerParameters, this, _playerData);
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

        public void ReceiveHit(float damage)
        {
            _playerController.TakeDamage(damage);
        }
    }
}