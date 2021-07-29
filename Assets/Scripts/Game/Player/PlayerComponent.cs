using Game.Physics;
using UnityEngine;

namespace Game.Player
{
    public class PlayerComponent: MonoBehaviour, IGameView
    {
        [SerializeField] 
        private PlayerDefinition playerParameters;

        [SerializeField] 
        private PhysicsBody2DDefinition bodyParameters;

        
        private PlayerController _playerController;

        private PlayerControls _controls;
        // private @Controls _controls;
        private void Start()
        {
            var size = GetComponent<SpriteRenderer>().bounds.size;
            
            var tunnelController = new ScreenTunnelLogic(Camera.main, transform, size);

            var body = new PhysicsBody2D(transform, bodyParameters, null);

            _playerController = new PlayerController(body, tunnelController, _controls, playerParameters, this);
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

        private void FixedUpdate()
        {
            _playerController.PhysicsUpdate(Time.fixedDeltaTime);
        }

        public Transform Transform => transform;
        public void DestroyGameObject()
        {
            
        }
    }
}